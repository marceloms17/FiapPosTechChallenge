using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Core.PosTech8Nett.Api.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Core.PosTech8Nett.Test.Steps
{
    [Binding]
    [Scope(Feature = "Game Service")]
    public class GameServiceSteps
    {
        private readonly Mock<IRepository<Game>> _gameRepositoryMock = new();
        private readonly GameService _gameService;
        private List<Game> _listaJogos;
        private IEnumerable<Game> _resultadoLista;
        private Game _jogo;
        private Guid _jogoId;

        public GameServiceSteps()
        {
            _gameService = new GameService(_gameRepositoryMock.Object);
        }

        [Given(@"que existem jogos cadastrados")]
        public void DadoQueExistemJogos()
        {
            _listaJogos = new List<Game> { new Game { Title = "Test Game" } };
            _gameRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(_listaJogos);
        }

        [When(@"o serviço requisita todos os jogos")]
        public async Task QuandoServicoBuscaTodos()
        {
            _resultadoLista = await _gameService.GetAllAsync();
        }

        [Then(@"o sistema deve retornar a lista com todos os jogos")]
        public void EntaoRetornaLista()
        {
            Assert.NotNull(_resultadoLista);
            Assert.Single(_resultadoLista);
        }

        [Given(@"que existe um jogo com ID válido")]
        public void DadoJogoPorId()
        {
            _jogoId = Guid.NewGuid();
            _jogo = new Game { Id = _jogoId, Title = "Game by ID" };
            _gameRepositoryMock.Setup(r => r.GetByIdAsync(_jogoId)).ReturnsAsync(_jogo);
        }

        [When(@"o serviço requisita o jogo por ID")]
        public async Task QuandoBuscaPorId()
        {
            _jogo = await _gameService.GetByIdAsync(_jogoId);
        }

        [Then(@"o sistema deve retornar o jogo correspondente")]
        public void EntaoRetornaJogoPorId()
        {
            Assert.NotNull(_jogo);
            Assert.Equal(_jogoId, _jogo.Id);
        }

        [Given(@"que um jogo novo foi criado")]
        public void DadoNovoJogo()
        {
            _jogo = new Game { Title = "Novo Jogo" };
        }

        [When(@"o serviço adiciona o jogo")]
        public async Task QuandoAdicionaJogo()
        {
            await _gameService.AddAsync(_jogo);
        }

        [Then(@"o repositório deve ser chamado para adicionar")]
        public void EntaoVerificaAdicao()
        {
            _gameRepositoryMock.Verify(r => r.AddAsync(_jogo), Times.Once);
        }

        [Given(@"que um jogo já existente foi alterado")]
        public void DadoJogoAlterado()
        {
            _jogo = new Game { Id = Guid.NewGuid(), Title = "Atualizado" };
        }

        [When(@"o serviço atualiza o jogo")]
        public void QuandoAtualiza()
        {
            _gameService.UpdateAsync(_jogo);
        }

        [Then(@"o repositório deve ser chamado para atualizar")]
        public void EntaoVerificaAtualizacao()
        {
            _gameRepositoryMock.Verify(r => r.Update(_jogo), Times.Once);
        }

        [Given(@"que existe um jogo com ID válido para exclusão")]
        public void DadoJogoParaExcluir()
        {
            _jogoId = Guid.NewGuid();
            _jogo = new Game { Id = _jogoId, Title = "Excluir" };

            _gameRepositoryMock.Setup(r => r.GetByIdAsync(_jogoId)).ReturnsAsync(_jogo);
        }

        [When(@"o serviço exclui o jogo")]
        public async Task QuandoExclui()
        {
            await _gameService.DeleteAsync(_jogoId);
        }

        [Then(@"o repositório deve ser chamado para excluir")]
        public void EntaoVerificaExclusao()
        {
            _gameRepositoryMock.Verify(r => r.Delete(_jogo), Times.Once);
        }
    }
}
