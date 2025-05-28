using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Core.PosTech8Nett.Api.Domain.Model.User.Responses;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;

namespace Core.PosTech8Nett.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserServices> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserServices>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar Ok com null quando o e-mail não for encontrado")]
        public async Task DeveRetornarOkComNull_QuandoEmailNaoForEncontrado()
        {
            // Arrange
            var request = new GetUserByEmailRequest { Email = "naoexiste@email.com" };
            _userServiceMock.Setup(x => x.GetByEmailAsync(It.Is<GetUserByEmailRequest>(r => r.Email == request.Email)))
                            .ReturnsAsync((UserResponse)null);

            // Act
            var resultado = await _controller.GetByEmail(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Null(okResult.Value); // Confirma que o valor retornado é realmente null
        }

        [Fact(DisplayName = "Deve retornar Ok com null quando o nickname não for encontrado")]
        public async Task DeveRetornarOkComNull_QuandoNickNameNaoForEncontrado()
        {
            // Arrange
            var request = new GetUserByNickNameRequest { NickName = "Inexistente" };
            _userServiceMock.Setup(x => x.GetByNickNameAsync(It.Is<GetUserByNickNameRequest>(r => r.NickName == request.NickName)))
                            .ReturnsAsync((UserResponse)null);

            // Act
            var resultado = await _controller.GetNickName(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Null(okResult.Value);
        }

        [Fact(DisplayName = "Deve retornar Ok com null quando o ID não for encontrado")]
        public async Task DeveRetornarOkComNull_QuandoIdNaoForEncontrado()
        {
            // Arrange
            var request = new GetUserByIdRequest { Id = new Guid() };
            _userServiceMock.Setup(x => x.GetByIdAsync(It.Is<GetUserByIdRequest>(r => r.Id == request.Id)))
                            .ReturnsAsync((UserResponse)null);

            // Act
            var resultado = await _controller.GetById(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Null(okResult.Value);
        }
    }
}