using AutoMapper;
using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Domain.Model.User.Responses;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Core.PosTech8Nett.Api.Services;
using Moq;
using Xunit;

namespace Core.PosTech8Nett.Test.Services.UserInformation
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserServices _service;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new UserServices(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Deve lançar exceção quando o BlockUserRequest for inválido")]
        public async Task DeveLancarExcecao_QuandoBlockUserRequestForInvalido()
        {
            // Arrange
            var request = new BlockUserRequest(); // inválido (sem ID)

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.BlockUserAsync(request));
            Assert.Contains("validation errors", excecao.Message);
        }

        [Fact(DisplayName = "Deve lançar exceção quando o usuário não for encontrado no bloqueio")]
        public async Task DeveLancarExcecao_QuandoUsuarioNaoForEncontrado_Bloqueio()
        {
            // Arrange
            var request = new BlockUserRequest { Id = Guid.NewGuid(), EnableBlocking = true };
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync((UsersEntitie)null);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.BlockUserAsync(request));
            Assert.Equal("Usuario não encontrado.", excecao.Message);
        }

        [Fact(DisplayName = "Deve bloquear o usuário com sucesso")]
        public async Task DeveBloquearUsuario_ComSucesso()
        {
            // Arrange
            var request = new BlockUserRequest { Id = Guid.NewGuid(), EnableBlocking = true };
            var usuario = new UsersEntitie { Id = request.Id };

            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync(usuario);

            _userRepositoryMock
                .Setup(x => x.BlockUserAsync(usuario, request.EnableBlocking))
                .Returns(Task.CompletedTask);

            // Act
            await _service.BlockUserAsync(request);

            // Assert
            _userRepositoryMock.Verify(x => x.BlockUserAsync(usuario, request.EnableBlocking), Times.Once);
        }
        [Fact(DisplayName = "Deve lançar exceção quando o DeleteUserRequest for inválido")]
        public async Task DeveLancarExcecao_QuandoDeleteUserRequestForInvalido()
        {
            // Arrange
            var request = new DeleteUserRequest(); // inválido

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(request));
            Assert.Contains("validation errors", excecao.Message);
        }

        [Fact(DisplayName = "Deve lançar exceção quando o usuário não for encontrado na exclusão")]
        public async Task DeveLancarExcecao_QuandoUsuarioNaoForEncontrado_Delete()
        {
            // Arrange
            var request = new DeleteUserRequest { Id = Guid.NewGuid() };
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync((UsersEntitie)null);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(request));
            Assert.Equal("Usuario não encontrado.", excecao.Message);
        }

        [Fact(DisplayName = "Deve deletar o usuário com sucesso")]
        public async Task DeveDeletarUsuario_ComSucesso()
        {
            // Arrange
            var request = new DeleteUserRequest { Id = Guid.NewGuid() };
            var usuario = new UsersEntitie { Id = request.Id };

            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync(usuario);

            _userRepositoryMock
                .Setup(x => x.DeleteAsync(usuario))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(request);

            // Assert
            _userRepositoryMock.Verify(x => x.DeleteAsync(usuario), Times.Once);
        }
        [Fact(DisplayName = "Deve criar um usuário com sucesso")]
        public async Task DeveCriarUsuario_ComSucesso()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Email = "teste@email.com",
                Password = "Senha@123"
            };

            var entidade = new UsersEntitie { Email = request.Email };

            _mapperMock
                .Setup(x => x.Map<UsersEntitie>(request))
                .Returns(entidade);

            _userRepositoryMock
                .Setup(x => x.CreateAsync(entidade, request.Password))
                .Returns(Task.CompletedTask);

            // Act
            await _service.CreateAsync(request);

            // Assert
            _userRepositoryMock.Verify(x => x.CreateAsync(entidade, request.Password), Times.Once);
        }

        [Fact(DisplayName = "Deve lançar exceção quando o GetUserByEmailRequest for inválido")]
        public async Task DeveLancarExcecao_QuandoGetUserByEmailRequestForInvalido()
        {
            // Arrange
            var request = new GetUserByEmailRequest { Email = "" }; // vazio = inválido

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.GetByEmailAsync(request));
            Assert.Contains("validation errors", excecao.Message);
        }

        [Fact(DisplayName = "Deve retornar UserResponse quando o e-mail for válido")]
        public async Task DeveRetornarUsuario_QuandoEmailForValido()
        {
            // Arrange
            var request = new GetUserByEmailRequest { Email = "teste@email.com" };
            var entidade = new UsersEntitie { Email = request.Email };
            var response = new UserResponse { Email = request.Email };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(entidade);

            _mapperMock
                .Setup(x => x.Map<UserResponse>(entidade))
                .Returns(response);

            // Act
            var resultado = await _service.GetByEmailAsync(request);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(request.Email, resultado.Email);
        }

        [Fact(DisplayName = "Deve lançar exceção quando o GetUserByIdRequest for inválido")]
        public async Task DeveLancarExcecao_QuandoGetUserByIdRequestForInvalido()
        {
            // Arrange
            var request = new GetUserByIdRequest(); // inválido

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(request));
            Assert.Contains("validation errors", excecao.Message);
        }

        [Fact(DisplayName = "Deve retornar UserResponse quando o ID for válido")]
        public async Task DeveRetornarUsuario_QuandoIdForValido()
        {
            // Arrange
            var request = new GetUserByIdRequest { Id = Guid.NewGuid() };
            var entidade = new UsersEntitie { Id = request.Id };
            var response = new UserResponse { Id = request.Id };

            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync(entidade);

            _mapperMock
                .Setup(x => x.Map<UserResponse>(entidade))
                .Returns(response);

            // Act
            var resultado = await _service.GetByIdAsync(request);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(request.Id, resultado.Id);
        }

        [Fact(DisplayName = "Deve lançar exceção quando o GetUserByNickNameRequest for inválido")]
        public async Task DeveLancarExcecao_QuandoGetUserByNickNameRequestForInvalido()
        {
            // Arrange
            var request = new GetUserByNickNameRequest(); // inválido

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.GetByNickNameAsync(request));
            Assert.Contains("validation errors", excecao.Message);
        }

        [Fact(DisplayName = "Deve retornar UserResponse quando o nickname for válido")]
        public async Task DeveRetornarUsuario_QuandoNickNameForValido()
        {
            // Arrange
            var request = new GetUserByNickNameRequest { NickName = "teste" };
            var entidade = new UsersEntitie { NickName = request.NickName };
            var response = new UserResponse { NickName = request.NickName };

            _userRepositoryMock
                .Setup(x => x.GetByNicknameAsync(request.NickName))
                .ReturnsAsync(entidade);

            _mapperMock
                .Setup(x => x.Map<UserResponse>(entidade))
                .Returns(response);

            // Act
            var resultado = await _service.GetByNickNameAsync(request);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(request.NickName, resultado.NickName);
        }


    }
}