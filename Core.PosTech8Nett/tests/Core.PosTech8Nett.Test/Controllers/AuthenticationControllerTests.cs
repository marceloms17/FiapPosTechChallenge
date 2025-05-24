using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Domain.Entities.Identity;

namespace Core.PosTech8Nett.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<UserManager<UsersEntitie>> _userManagerMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            var store = new Mock<IUserStore<UsersEntitie>>();
            _userManagerMock = new Mock<UserManager<UsersEntitie>>(store.Object, null, null, null, null, null, null, null, null);
            _configMock = new Mock<IConfiguration>();

            // Setup JWT settings
            _configMock.Setup(x => x.GetSection("JwtSettings")["SecretKey"]).Returns("supersecretkey123456789012345678901234567890");
            _configMock.Setup(x => x.GetSection("JwtSettings")["Issuer"]).Returns("https://PosTech8NettApp.com");
            _configMock.Setup(x => x.GetSection("JwtSettings")["Audience"]).Returns("https://Api.PosTech8NettApp.com");

            _controller = new AuthenticationController(_userManagerMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task DadoUsuarioValido_QuandoLogin_EntaoRetornaToken()
        {
            // Given
            var user = new UsersEntitie { Id = System.Guid.NewGuid(), Email = "admin@admin.com", UserName = "admin@admin.com" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, "Admin@123")).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Admin" });

            var loginModel = new LoginModel { Email = user.Email, Password = "Admin@123" };

            // When
            var result = await _controller.Login(loginModel);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result);

            var tokenProperty = okResult.Value?.GetType().GetProperty("token");
            var tokenValue = tokenProperty?.GetValue(okResult.Value, null)?.ToString();

            Assert.False(string.IsNullOrWhiteSpace(tokenValue));
        }


        [Fact]
        public async Task DadoEmailInexistente_QuandoLogin_EntaoRetornaUnauthorized()
        {
            // Given
            _userManagerMock.Setup(x => x.FindByEmailAsync("notfound@email.com")).ReturnsAsync((UsersEntitie)null);

            var loginModel = new LoginModel { Email = "notfound@email.com", Password = "qualquer" };

            // When
            var result = await _controller.Login(loginModel);

            // Then
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task DadoSenhaIncorreta_QuandoLogin_EntaoRetornaUnauthorized()
        {
            // Given
            var user = new UsersEntitie { Id = System.Guid.NewGuid(), Email = "user@test.com" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, "errada")).ReturnsAsync(false);

            var loginModel = new LoginModel { Email = user.Email, Password = "errada" };

            // When
            var result = await _controller.Login(loginModel);

            // Then
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
