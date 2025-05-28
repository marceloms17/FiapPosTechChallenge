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

        [Fact(DisplayName = "Deve retornar OK com a lista de jogos mapeados")]
        public async Task GetAll_ShouldReturnOkWithMappedGames()
        {
            var games = new List<Game> { new Game { Title = "Test Game" } };
            var responses = new List<GameResponse> { new GameResponse { Title = "Test Game" } };

            _gameServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(games);
            _mapperMock.Setup(m => m.Map<IEnumerable<GameResponse>>(games)).Returns(responses);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(responses, okResult.Value);
        }

        [Fact(DisplayName = "Deve retornar NotFound quando o jogo não for encontrado")]
        public async Task GetById_ShouldReturnNotFound_WhenGameIsNull()
        {
            _gameServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Game)null);

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Deve retornar OK com o jogo mapeado")]
        public async Task GetById_ShouldReturnOkWithMappedGame()
        {
            var game = new Game { Id = Guid.NewGuid(), Title = "Test" };
            var response = new GameResponse { Title = "Test" };

            _gameServiceMock.Setup(s => s.GetByIdAsync(game.Id)).ReturnsAsync(game);
            _mapperMock.Setup(m => m.Map<GameResponse>(game)).Returns(response);

            var result = await _controller.GetById(game.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact(DisplayName = "Deve retornar CreatedAtAction ao criar jogo")]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            var request = new GameRequest { Title = "New Game" };
            var game = new Game { Id = Guid.NewGuid(), Title = "New Game" };

            _mapperMock.Setup(m => m.Map<Game>(request)).Returns(game);
            _gameServiceMock.Setup(s => s.AddAsync(game)).Returns(Task.CompletedTask);

            var result = await _controller.Create(request);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetById), createdResult.ActionName);
        }

        [Fact(DisplayName = "Deve retornar NoContent ao atualizar jogo")]
        public async Task Update_ShouldReturnNoContent()
        {
            var id = Guid.NewGuid();
            var request = new GameRequest { Title = "Updated Game" };
            var game = new Game { Id = id, Title = "Updated Game" };

            _mapperMock.Setup(m => m.Map<Game>(request)).Returns(game);
            _gameServiceMock.Setup(s => s.UpdateAsync(game)).Returns(Task.CompletedTask);

            var result = await _controller.Update(id, request);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact(DisplayName = "Deve retornar NoContent ao deletar jogo")]
        public async Task Delete_ShouldReturnNoContent()
        {
            var id = Guid.NewGuid();
            _gameServiceMock.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
