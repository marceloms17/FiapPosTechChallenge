using AutoMapper;
using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Domain.Model.User.Responses;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Core.PosTech8Nett.Api.Services;
using Moq;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Core.PosTech8Nett.Test.Steps
{
    [Binding]
    public class UserServiceSteps
    {
        private readonly ScenarioContext _context;
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly UserServices _service;

        private BlockUserRequest _blockRequest;
        private DeleteUserRequest _deleteRequest;
        private CreateUserRequest _createRequest;
        private GetUserByEmailRequest _emailRequest;
        private GetUserByIdRequest _idRequest;
        private GetUserByNickNameRequest _nicknameRequest;

        private UsersEntitie _usuario;
        private UserResponse _userResponse;
        private Exception _exception;
        private UserResponse _resultado;

        public UserServiceSteps(ScenarioContext context)
        {
            _context = context;
            _service = new UserServices(_userRepositoryMock.Object, _mapperMock.Object);
        }

        // BLOQUEIO
        [Given(@"que existe um usuário com ID válido e bloqueio habilitado")]
        public void DadoUsuarioValidoComBloqueio()
        {
            _blockRequest = new BlockUserRequest { Id = Guid.NewGuid(), EnableBlocking = true };
            _usuario = new UsersEntitie { Id = _blockRequest.Id };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(_blockRequest.Id)).ReturnsAsync(_usuario);
            _userRepositoryMock.Setup(x => x.BlockUserAsync(_usuario, true)).Returns(Task.CompletedTask);
        }

        [When(@"o serviço de usuário solicita o bloqueio")]
        public async Task QuandoServicoBloqueia()
        {
            try
            {
                await _service.BlockUserAsync(_blockRequest);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"o repositório deve ser chamado para bloquear o usuário")]
        public void EntaoRepositorioChamadoParaBloquear()
        {
            _userRepositoryMock.Verify(x => x.BlockUserAsync(_usuario, true), Times.Once);
            Assert.Null(_exception);
        }

        // DELETE
        [Given(@"que existe um usuário com ID válido para exclusão")]
        public void DadoUsuarioValidoParaExclusao()
        {
            _deleteRequest = new DeleteUserRequest { Id = Guid.NewGuid() };
            _usuario = new UsersEntitie { Id = _deleteRequest.Id };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(_deleteRequest.Id)).ReturnsAsync(_usuario);
            _userRepositoryMock.Setup(x => x.DeleteAsync(_usuario)).Returns(Task.CompletedTask);
        }

        [When(@"o serviço de usuário solicita a exclusão")]
        public async Task QuandoServicoSolicitaExclusao()
        {
            try
            {
                await _service.DeleteAsync(_deleteRequest);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"o repositório deve ser chamado para deletar o usuário")]
        public void EntaoRepositorioChamadoParaDeletar()
        {
            _userRepositoryMock.Verify(x => x.DeleteAsync(_usuario), Times.Once);
            Assert.Null(_exception);
        }

        // CREATE
        [Given(@"que os dados para criação do usuário são válidos")]
        public void DadoDadosValidosParaCriacao()
        {
            _createRequest = new CreateUserRequest
            {
                Email = "teste@email.com",
                Password = "Senha@123"
            };

            _usuario = new UsersEntitie { Email = _createRequest.Email };

            _mapperMock.Setup(m => m.Map<UsersEntitie>(_createRequest)).Returns(_usuario);
            _userRepositoryMock.Setup(r => r.CreateAsync(_usuario, _createRequest.Password)).Returns(Task.CompletedTask);
        }

        [When(@"o serviço de usuário solicita a criação")]
        public async Task QuandoServicoSolicitaCriacao()
        {
            try
            {
                await _service.CreateAsync(_createRequest);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"o repositório deve ser chamado para criar o usuário")]
        public void EntaoRepositorioChamadoParaCriar()
        {
            _userRepositoryMock.Verify(x => x.CreateAsync(_usuario, _createRequest.Password), Times.Once);
            Assert.Null(_exception);
        }

        // GET BY EMAIL
        [Given(@"que existe um usuário com o e-mail informado")]
        public void DadoUsuarioComEmailInformado()
        {
            _emailRequest = new GetUserByEmailRequest { Email = "teste@email.com" };
            _usuario = new UsersEntitie { Email = _emailRequest.Email };
            _userResponse = new UserResponse { Email = _emailRequest.Email };

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(_emailRequest.Email)).ReturnsAsync(_usuario);
            _mapperMock.Setup(x => x.Map<UserResponse>(_usuario)).Returns(_userResponse);
        }

        [When(@"o serviço de usuário requisita pelo e-mail")]
        public async Task QuandoServicoRequisitaPorEmail()
        {
            _resultado = await _service.GetByEmailAsync(_emailRequest);
        }

        [Then(@"o sistema deve retornar os dados do usuário pelo e-mail")]
        public void EntaoRetornaUsuarioPorEmail()
        {
            Assert.NotNull(_resultado);
            Assert.Equal(_emailRequest.Email, _resultado.Email);
        }

        // GET BY ID
        [Given(@"que existe um usuário com o ID informado")]
        public void DadoUsuarioComIdInformado()
        {
            _idRequest = new GetUserByIdRequest { Id = Guid.NewGuid() };
            _usuario = new UsersEntitie { Id = _idRequest.Id };
            _userResponse = new UserResponse { Id = _idRequest.Id };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(_idRequest.Id)).ReturnsAsync(_usuario);
            _mapperMock.Setup(x => x.Map<UserResponse>(_usuario)).Returns(_userResponse);
        }

        [When(@"o serviço de usuário requisita pelo ID")]
        public async Task QuandoServicoRequisitaPorId()
        {
            _resultado = await _service.GetByIdAsync(_idRequest);
        }

        [Then(@"o sistema deve retornar os dados do usuário pelo ID")]
        public void EntaoRetornaUsuarioPorId()
        {
            Assert.NotNull(_resultado);
            Assert.Equal(_idRequest.Id, _resultado.Id);
        }

        // GET BY NICKNAME
        [Given(@"que existe um usuário com o nickname informado")]
        public void DadoUsuarioComNicknameInformado()
        {
            _nicknameRequest = new GetUserByNickNameRequest { NickName = "tester" };
            _usuario = new UsersEntitie { NickName = _nicknameRequest.NickName };
            _userResponse = new UserResponse { NickName = _nicknameRequest.NickName };

            _userRepositoryMock.Setup(x => x.GetByNicknameAsync(_nicknameRequest.NickName)).ReturnsAsync(_usuario);
            _mapperMock.Setup(x => x.Map<UserResponse>(_usuario)).Returns(_userResponse);
        }

        [When(@"o serviço de usuário requisita pelo nickname")]
        public async Task QuandoServicoRequisitaPorNickname()
        {
            _resultado = await _service.GetByNickNameAsync(_nicknameRequest);
        }

        [Then(@"o sistema deve retornar os dados do usuário pelo nickname")]
        public void EntaoRetornaUsuarioPorNickname()
        {
            Assert.NotNull(_resultado);
            Assert.Equal(_nicknameRequest.NickName, _resultado.NickName);
        }
    }
}
