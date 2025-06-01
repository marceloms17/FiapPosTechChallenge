using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.Authenticator;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Core.PosTech8Nett.Api.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Core.PosTech8Nett.Test.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AuthenticationServices _service;

        public AuthenticationServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _service = new AuthenticationServices(_configurationMock.Object, _userRepositoryMock.Object);
        }

        [Fact(DisplayName = "Deve lançar exceção quando o LoginRequest for inválido")]
        public async Task DeveLancarExcecao_QuandoLoginRequestForInvalido()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "", // inválido
                Password = "" // inválido
            };

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.LoginAsync(request));
            Assert.Contains("validation errors", excecao.Message);
        }

        [Fact(DisplayName = "Deve retornar null quando o usuário não existir")]
        public async Task DeveRetornarNull_QuandoUsuarioNaoExistir()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "naoexiste@email.com",
                Password = "Senha123@"
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((UsersEntitie)null);

            // Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.LoginAsync(request));
            Assert.Contains("Usuário não encontrado", excecao.Message);
        }

        [Fact(DisplayName = "Deve retornar null quando a senha estiver incorreta")]
        public async Task DeveRetornarNull_QuandoSenhaEstiverIncorreta()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "teste@email.com",
                Password = "SenhaIncorreta"
            };

            var user = new UsersEntitie { Email = request.Email };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(user);

            _userRepositoryMock
                .Setup(x => x.CheckPasswordAsync(user, request.Password))
                .ReturnsAsync(false);

            // Act
            var resultado = await _service.LoginAsync(request);

            // Assert
            Assert.Null(resultado);
        }

        [Fact(DisplayName = "Deve retornar token quando o login for válido")]
        public async Task DeveRetornarToken_QuandoLoginForValido()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "teste@email.com",
                Password = "Senha123@"
            };

            var user = new UsersEntitie { Email = request.Email, Id = Guid.NewGuid() };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(user);

            _userRepositoryMock
                .Setup(x => x.CheckPasswordAsync(user, request.Password))
                .ReturnsAsync(true);

            _userRepositoryMock
                .Setup(x => x.GetRolesUser(user))
                .ReturnsAsync(new List<string> { "User" });

            var jwtSectionMock = new Mock<IConfigurationSection>();
            jwtSectionMock.Setup(s => s["SecretKey"]).Returns("12345678901234567890123456789012"); // 32 chars
            jwtSectionMock.Setup(s => s["Issuer"]).Returns("TestIssuer");
            jwtSectionMock.Setup(s => s["Audience"]).Returns("TestAudience");

            _configurationMock.Setup(c => c.GetSection("JwtSettings")).Returns(jwtSectionMock.Object);

            // Act
            var token = await _service.LoginAsync(request);

            // Assert
            Assert.NotNull(token);
            Assert.IsType<string>(token);
            Assert.NotEmpty(token);
        }

    }
}