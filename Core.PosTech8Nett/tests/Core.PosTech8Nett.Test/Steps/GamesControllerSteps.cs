using AutoMapper;
using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Core.PosTech8Nett.Api.Domain.Model.Game;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Core.PosTech8Nett.Test.Steps
{
    [Binding]
    [Scope(Feature = "Games Controller")]
    public class GamesControllerSteps
    {
        private readonly Mock<IGameService> _gameServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly GamesController _controller;
        private IActionResult _resultado;
        private GameRequest _gameRequest;
        private Guid _gameId;
        private Game _game;
        private GameResponse _response;
        private List<Game> _games;
        private List<GameResponse> _mappedResponses;

        public GamesControllerSteps()
        {
            _controller = new GamesController(_gameServiceMock.Object, _mapperMock.Object);
        }

        [Given(@"que existem jogos cadastrados no sistema")]
        public void DadoQueExistemJogosCadastrados()
        {
            _games = new List<Game> { new Game { Title = "Test Game" } };
            _mappedResponses = new List<GameResponse> { new GameResponse { Title = "Test Game" } };

            _gameServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(_games);
            _mapperMock.Setup(m => m.Map<IEnumerable<GameResponse>>(_games)).Returns(_mappedResponses);
        }

        [When(@"o usuário requisita todos os jogos")]
        public async Task QuandoUsuarioRequisitaTodosOsJogos()
        {
            _resultado = await _controller.GetAll();
        }

        [Then(@"o sistema deve retornar OK com a lista mapeada")]
        public void EntaoRetornaOkComListaMapeada()
        {
            var ok = Assert.IsType<OkObjectResult>(_resultado);
            Assert.Equal(_mappedResponses, ok.Value);
        }

        [Given(@"que nenhum jogo corresponde ao ID informado")]
        public void DadoJogoNaoExiste()
        {
            _gameServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Game)null);
        }

        [When(@"o usuário requisita o jogo por ID")]
        public async Task QuandoUsuarioBuscaPorId()
        {
            _resultado = await _controller.GetById(_gameId); 
        }

        [Then(@"o sistema deve retornar NotFound")]
        public void EntaoRetornaNotFound()
        {
            Assert.IsType<NotFoundResult>(_resultado);
        }

        [Given(@"que existe um jogo com ID válido")]
        public void DadoJogoExiste()
        {
            _gameId = Guid.NewGuid();
            _game = new Game { Id = _gameId, Title = "Test" };
            _response = new GameResponse { Title = "Test" };

            _gameServiceMock.Setup(s => s.GetByIdAsync(_gameId)).ReturnsAsync(_game);
            _mapperMock.Setup(m => m.Map<GameResponse>(_game)).Returns(_response);
        }

        [Then(@"o sistema deve retornar OK com o jogo mapeado")]
        public void EntaoRetornaOkComJogo()
        {
            var ok = Assert.IsType<OkObjectResult>(_resultado);
            Assert.Equal(_response, ok.Value);
        }

        [Given(@"que o usuário envia dados válidos de um novo jogo")]
        public void DadoNovoJogo()
        {
            _gameRequest = new GameRequest { Title = "Novo Jogo" };
            _game = new Game { Id = Guid.NewGuid(), Title = "Novo Jogo" };

            _mapperMock.Setup(m => m.Map<Game>(_gameRequest)).Returns(_game);
            _gameServiceMock.Setup(s => s.AddAsync(_game)).Returns(Task.CompletedTask);
        }

        [When(@"o usuário solicita a criação do jogo")]
        public async Task QuandoCriaJogo()
        {
            _resultado = await _controller.Create(_gameRequest);
        }

        [Then(@"o sistema deve retornar CreatedAtAction")]
        public void EntaoRetornaCreated()
        {
            var created = Assert.IsType<CreatedAtActionResult>(_resultado);
            Assert.Equal(nameof(_controller.GetById), created.ActionName);
        }

        [Given(@"que o usuário envia dados atualizados de um jogo")]
        public void DadoAtualizacaoDeJogo()
        {
            _gameId = Guid.NewGuid();
            _gameRequest = new GameRequest { Title = "Atualizado" };
            _game = new Game { Id = _gameId, Title = "Atualizado" };

            _mapperMock.Setup(m => m.Map<Game>(_gameRequest)).Returns(_game);
            _gameServiceMock.Setup(s => s.UpdateAsync(_game)).Returns(Task.CompletedTask);
        }

        [When(@"o usuário solicita a atualização do jogo")]
        public async Task QuandoAtualizaJogo()
        {
            _resultado = await _controller.Update(_gameId, _gameRequest);
        }

        [Then(@"o sistema deve retornar NoContent")]
        public void EntaoRetornaNoContent()
        {
            Assert.IsType<NoContentResult>(_resultado);
        }

        [When(@"o usuário solicita a exclusão do jogo")]
        public async Task QuandoUsuarioDeletaJogo()
        {
            _gameId = Guid.NewGuid();
            _gameServiceMock.Setup(s => s.DeleteAsync(_gameId)).Returns(Task.CompletedTask);
            _resultado = await _controller.Delete(_gameId);
        }
    }
}
