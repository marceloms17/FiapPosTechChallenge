using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Moq;
using Xunit;

namespace Core.PosTech8Nett.Test.Services.GameInformation
{
    public class GameServiceTests
    {
        private readonly Mock<IRepository<Game>> _gameRepositoryMock;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _gameRepositoryMock = new Mock<IRepository<Game>>();
            _gameService = new GameService(_gameRepositoryMock.Object);
        }

        [Fact(DisplayName = "Deve retornar todos os jogos cadastrados")]
        public async Task GetAllAsync_ShouldReturnAllGames()
        {
            var games = new List<Game> { new Game { Title = "Test Game" } };
            _gameRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(games);

            var result = await _gameService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact(DisplayName = "Deve retornar jogo quando ID existir")]
        public async Task GetByIdAsync_ShouldReturnGame_WhenExists()
        {
            var gameId = Guid.NewGuid();
            var game = new Game { Id = gameId, Title = "Test Game" };
            _gameRepositoryMock.Setup(r => r.GetByIdAsync(gameId)).ReturnsAsync(game);

            var result = await _gameService.GetByIdAsync(gameId);

            Assert.NotNull(result);
            Assert.Equal(gameId, result.Id);
        }

        [Fact(DisplayName = "Deve adicionar novo jogo")]
        public async Task AddAsync_ShouldAddGame()
        {
            var game = new Game { Title = "New Game" };
            await _gameService.AddAsync(game);

            _gameRepositoryMock.Verify(r => r.AddAsync(game), Times.Once);
        }

        [Fact(DisplayName = "Deve atualizar jogo existente")]
        public void Update_ShouldUpdateGame()
        {
            var game = new Game { Title = "Update Game" };
            _gameService.UpdateAsync(game);

            _gameRepositoryMock.Verify(r => r.Update(game), Times.Once);
        }

        [Fact(DisplayName = "Deve remover jogo quando ID existir")]
        public async Task Delete_ShouldDeleteGame()
        {
            var gameId = Guid.NewGuid();
            var game = new Game { Id = gameId, Title = "Delete Game" };

            _gameRepositoryMock.Setup(r => r.GetByIdAsync(gameId)).ReturnsAsync(game);

            await _gameService.DeleteAsync(gameId);

            _gameRepositoryMock.Verify(r => r.Delete(game), Times.Once);
        }
    }
}
