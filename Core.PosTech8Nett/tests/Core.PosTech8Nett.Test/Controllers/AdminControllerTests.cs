using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Core.PosTech8Nett.Test.Controllers
{
    public class AdminControllerTests
    {
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _userServiceMock = new Mock<IUserServices>();
            _controller = new AdminController(_userServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar NoContent ao bloquear um usuário")]
        public async Task DeveRetornarNoContent_QuandoBloquearUsuario()
        {
            // Arrange
            var request = new BlockUserRequest { Id = new Guid() };
            _userServiceMock.Setup(x => x.BlockUserAsync(request)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.BlockUser(request);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact(DisplayName = "Deve retornar NoContent ao deletar um usuário")]
        public async Task DeveRetornarNoContent_QuandoDeletarUsuario()
        {
            // Arrange
            var request = new DeleteUserRequest { Id = new Guid() };
            _userServiceMock.Setup(x => x.DeleteAsync(request)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.DeleteUser(request);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }
    }
}