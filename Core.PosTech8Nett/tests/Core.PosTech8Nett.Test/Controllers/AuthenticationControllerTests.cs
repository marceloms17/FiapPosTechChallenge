using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Domain.Model.Authenticator;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Core.PosTech8Nett.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IUserServices> _userServicesMock;
        private readonly Mock<IAuthenticationServices> _authServicesMock;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _userServicesMock = new Mock<IUserServices>();
            _authServicesMock = new Mock<IAuthenticationServices>();
            _controller = new AuthenticationController(_userServicesMock.Object, _authServicesMock.Object);
        }

        [Fact]
        public async Task DadoUsuarioValido_QuandoLogin_EntaoRetornaToken()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "admin@admin.com", Password = "Admin@123" };
            var expectedToken = "fake-jwt-token";

            _authServicesMock
                .Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync(expectedToken);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedToken, okResult.Value);
        }

        [Fact]
        public async Task DadoEmailInexistente_QuandoLogin_EntaoRetornaUnauthorized()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "notfound@email.com", Password = "qualquer" };

            _authServicesMock
                .Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync((string)null);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task DadoSenhaIncorreta_QuandoLogin_EntaoRetornaUnauthorized()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "user@test.com", Password = "wrongpassword" };

            _authServicesMock
                .Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync((string)null);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}