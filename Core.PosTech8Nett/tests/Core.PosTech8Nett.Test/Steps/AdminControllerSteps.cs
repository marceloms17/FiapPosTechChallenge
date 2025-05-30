using System;
using System.Threading.Tasks;
using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace Core.PosTech8Nett.Test.Steps
{
    [Binding]
    [Scope(Feature = "Admin Controller")]
    public class AdminControllerSteps
    {
        private readonly ScenarioContext _context;
        private readonly Mock<IUserServices> _userServiceMock = new();
        private readonly AdminController _controller;
        private IActionResult _resultado;
        private BlockUserRequest _blockRequest;
        private DeleteUserRequest _deleteRequest;

        public AdminControllerSteps(ScenarioContext context)
        {
            _context = context;
            _controller = new AdminController(_userServiceMock.Object);
        }

        [Given(@"que existe um usuário com ID válido")]
        public void DadoQueExisteUmUsuarioComIDValido()
        {
            var id = Guid.NewGuid();
            _blockRequest = new BlockUserRequest { Id = id };
            _deleteRequest = new DeleteUserRequest { Id = id };

            _userServiceMock
                .Setup(s => s.BlockUserAsync(It.Is<BlockUserRequest>(r => r.Id == id)))
                .Returns(Task.CompletedTask);

            _userServiceMock
                .Setup(s => s.DeleteAsync(It.Is<DeleteUserRequest>(r => r.Id == id)))
                .Returns(Task.CompletedTask);
        }

        [When(@"o administrador solicita o bloqueio do usuário")]
        public async Task QuandoOAdministradorSolicitaOBloqueioDoUsuario()
        {
            _resultado = await _controller.BlockUser(_blockRequest);
        }

        [When(@"o administrador solicita a exclusão do usuário")]
        public async Task QuandoOAdministradorSolicitaAExclusaoDoUsuario()
        {
            _resultado = await _controller.DeleteUser(_deleteRequest);
        }

        [Then(@"o sistema deve retornar NoContent")]
        public void EntaoOSistemaDeveRetornarNoContent()
        {
            Assert.IsType<NoContentResult>(_resultado);
        }
    }
}
