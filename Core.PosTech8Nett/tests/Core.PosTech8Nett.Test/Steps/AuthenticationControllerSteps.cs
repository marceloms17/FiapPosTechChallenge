using System.Threading.Tasks;
using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Domain.Model.Authenticator;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace Core.PosTech8Nett.Test.Steps
{
    [Binding]
    [Scope(Feature = "Authentication Controller")]
    public class AuthenticationControllerSteps
    {
        private readonly Mock<IUserServices> _userServiceMock = new();
        private readonly Mock<IAuthenticationServices> _authServiceMock = new();
        private readonly AuthenticationController _controller;
        private LoginRequest _loginRequest;
        private IActionResult _resultado;
        private string _tokenEsperado;

        public AuthenticationControllerSteps()
        {
            _controller = new AuthenticationController(_userServiceMock.Object, _authServiceMock.Object);
        }

        [Given(@"que existe um usuário com e-mail e senha válidos")]
        public void DadoQueExisteUsuarioComEmailESenhaValidos()
        {
            _loginRequest = new LoginRequest { Email = "admin@admin.com", Password = "Admin@123" };
            _tokenEsperado = "fake-jwt-token";

            _authServiceMock
                .Setup(x => x.LoginAsync(It.Is<LoginRequest>(r =>
                    r.Email == _loginRequest.Email && r.Password == _loginRequest.Password)))
                .ReturnsAsync(_tokenEsperado);
        }

        [Given(@"que o e-mail informado não está cadastrado")]
        public void DadoEmailInexistente()
        {
            var loginRequest = new LoginRequest { Email = "notfound@email.com", Password = "qualquer" };

            _authServiceMock
                .Setup(x => x.LoginAsync(It.Is<LoginRequest>(r =>
                    r.Email == loginRequest.Email && r.Password == loginRequest.Password)))
                .ReturnsAsync((string)null);

            _loginRequest = loginRequest;
        }

        [Given(@"que o e-mail está correto mas a senha está errada")]
        public void DadoSenhaIncorreta()
        {
            var loginRequest = new LoginRequest { Email = "user@test.com", Password = "wrongpassword" };

            _authServiceMock
                .Setup(x => x.LoginAsync(It.Is<LoginRequest>(r =>
                    r.Email == loginRequest.Email && r.Password == loginRequest.Password)))
                .ReturnsAsync((string)null);

            _loginRequest = loginRequest;
        }

        [When(@"o usuário realiza login")]
        public async Task QuandoUsuarioRealizaLogin()
        {
            _resultado = await _controller.Login(_loginRequest);
        }

        [Then(@"o sistema deve retornar um token")]
        public void EntaoRetornaToken()
        {
            var okResult = Assert.IsType<OkObjectResult>(_resultado);
            Assert.Equal(_tokenEsperado, okResult.Value);
        }

        [Then(@"o sistema deve retornar Unauthorized")]
        public void EntaoRetornaUnauthorized()
        {
            Assert.IsType<UnauthorizedResult>(_resultado);
        }
    }
}
