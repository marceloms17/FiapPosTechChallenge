using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.Authenticator;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Core.PosTech8Nett.Api.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Core.PosTech8Nett.Test.Steps
{
    [Binding]
    [Scope(Feature = "Authentication Service")]
    public class AuthenticationServiceSteps
    {
        private readonly Mock<IConfiguration> _configurationMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly AuthenticationServices _service;
        private LoginRequest _request;
        private string _resultado;
        private Exception _excecaoCapturada;

        public AuthenticationServiceSteps()
        {
            _service = new AuthenticationServices(_configurationMock.Object, _userRepositoryMock.Object);
        }

        [Given(@"um LoginRequest com email e senha inválidos")]
        public void DadoLoginRequestInvalido()
        {
            _request = new LoginRequest { Email = "", Password = "" };
        }

        [Given(@"um LoginRequest com usuário não cadastrado")]
        public void DadoLoginComUsuarioNaoCadastrado()
        {
            _request = new LoginRequest { Email = "naoexiste@email.com", Password = "Senha123@" };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(_request.Email))
                .ReturnsAsync((UsersEntitie)null);
        }

        [Given(@"um LoginRequest com senha incorreta")]
        public void DadoLoginComSenhaIncorreta()
        {
            _request = new LoginRequest { Email = "teste@email.com", Password = "SenhaErrada" };

            var user = new UsersEntitie { Email = _request.Email };
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(_request.Email)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.CheckPasswordAsync(user, _request.Password)).ReturnsAsync(false);
        }

        [Given(@"um LoginRequest válido")]
        public void DadoLoginValido()
        {
            _request = new LoginRequest { Email = "teste@email.com", Password = "Senha123@" };
            var user = new UsersEntitie { Id = Guid.NewGuid(), Email = _request.Email };

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(_request.Email)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.CheckPasswordAsync(user, _request.Password)).ReturnsAsync(true);
            _userRepositoryMock.Setup(r => r.GetRolesUser(user)).ReturnsAsync(new List<string> { "User" });

            var jwtSectionMock = new Mock<IConfigurationSection>();
            jwtSectionMock.Setup(s => s["SecretKey"]).Returns("12345678901234567890123456789012");
            jwtSectionMock.Setup(s => s["Issuer"]).Returns("TestIssuer");
            jwtSectionMock.Setup(s => s["Audience"]).Returns("TestAudience");

            _configurationMock.Setup(c => c.GetSection("JwtSettings")).Returns(jwtSectionMock.Object);
        }

        [When(@"o serviço de autenticação é chamado")]
        public async Task QuandoServicoDeAutenticacaoChamado()
        {
            try
            {
                _resultado = await _service.LoginAsync(_request);
            }
            catch (Exception ex)
            {
                _excecaoCapturada = ex;
            }
        }

        [Then(@"o sistema deve lançar uma exceção de validação")]
        public void EntaoDeveLancarExcecao()
        {
            Assert.NotNull(_excecaoCapturada);
            Assert.Contains("validation errors", _excecaoCapturada.Message);
        }

        [Then(@"o resultado deve ser null")]
        public void EntaoResultadoDeveSerNull()
        {
            Assert.Null(_resultado);
        }

        [Then(@"o sistema deve retornar um token")]
        public void EntaoDeveRetornarToken()
        {
            Assert.NotNull(_resultado);
            Assert.IsType<string>(_resultado);
            Assert.NotEmpty(_resultado);
        }
    }
}
