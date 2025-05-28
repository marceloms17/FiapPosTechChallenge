using AutoMapper;
using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Core.PosTech8Nett.Api.Domain.Model.Game;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Core.PosTech8Nett.Test.Controllers
{
    public class GamesControllerTests
    {
        private readonly Mock<IGameService> _gameServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GamesController _controller;

        public GamesControllerTests()
        {
            _gameServiceMock = new Mock<IGameService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new GamesController(_gameServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithMappedGames()
        {
            // Arrange
            var games = new List<Game> { new Game { Title = "Test Game" } };
            var responses = new List<GameResponse> { new GameResponse { Title = "Test Game" } };

            _gameServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(games);
            _mapperMock.Setup(m => m.Map<IEnumerable<GameResponse>>(games)).Returns(responses);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(responses, okResult.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenGameIsNull()
        {
            // Arrange
            _gameServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Game)null);

            // Act
            var result = await _controller.GetById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkWithMappedGame()
        {
            // Arrange
            var game = new Game { Id = Guid.NewGuid(), Title = "Test" };
            var response = new GameResponse { Title = "Test" };

            _gameServiceMock.Setup(s => s.GetByIdAsync(game.Id)).ReturnsAsync(game);
            _mapperMock.Setup(m => m.Map<GameResponse>(game)).Returns(response);

            // Act
            var result = await _controller.GetById(game.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var request = new GameRequest { Title = "New Game" };
            var game = new Game { Id = Guid.NewGuid(), Title = "New Game" };

            _mapperMock.Setup(m => m.Map<Game>(request)).Returns(game);
            _gameServiceMock.Setup(s => s.AddAsync(game)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetById), createdResult.ActionName);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new GameRequest { Title = "Updated Game" };
            var game = new Game { Id = id, Title = "Updated Game" };

            _mapperMock.Setup(m => m.Map<Game>(request)).Returns(game);
            _gameServiceMock.Setup(s => s.UpdateAsync(game)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(id, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            _gameServiceMock.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}