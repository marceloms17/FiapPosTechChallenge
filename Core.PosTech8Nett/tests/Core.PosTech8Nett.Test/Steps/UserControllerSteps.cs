using Core.PosTech8Nett.Api.Controllers.V1;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Domain.Model.User.Responses;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Core.PosTech8Nett.Test.Steps
{
    [Binding]
    [Scope(Feature = "User Controller")]
    public class UserControllerSteps
    {
        private readonly Mock<IUserServices> _userServiceMock = new();
        private readonly UserController _controller;
        private IActionResult _resultado;
        private GetUserByEmailRequest _emailRequest;
        private GetUserByNickNameRequest _nicknameRequest;
        private GetUserByIdRequest _idRequest;

        public UserControllerSteps()
        {
            _controller = new UserController(_userServiceMock.Object);
        }

        [Given(@"que o e-mail informado não está cadastrado")]
        public void DadoEmailInexistente()
        {
            _emailRequest = new GetUserByEmailRequest { Email = "naoexiste@email.com" };
            _userServiceMock.Setup(x =>
                x.GetByEmailAsync(It.Is<GetUserByEmailRequest>(r => r.Email == _emailRequest.Email)))
                .ReturnsAsync((UserResponse)null);
        }

        [When(@"o cliente requisita o usuário por e-mail")]
        public async Task QuandoRequisitaUsuarioPorEmail()
        {
            _resultado = await _controller.GetByEmail(_emailRequest);
        }

        [Given(@"que o nickname informado não está cadastrado")]
        public void DadoNickNameInexistente()
        {
            _nicknameRequest = new GetUserByNickNameRequest { NickName = "Inexistente" };
            _userServiceMock.Setup(x =>
                x.GetByNickNameAsync(It.Is<GetUserByNickNameRequest>(r => r.NickName == _nicknameRequest.NickName)))
                .ReturnsAsync((UserResponse)null);
        }

        [When(@"o cliente requisita o usuário por nickname")]
        public async Task QuandoRequisitaUsuarioPorNickName()
        {
            _resultado = await _controller.GetNickName(_nicknameRequest);
        }

        [Given(@"que o ID informado não está cadastrado")]
        public void DadoIdInexistente()
        {
            _idRequest = new GetUserByIdRequest { Id = Guid.NewGuid() };
            _userServiceMock.Setup(x =>
                x.GetByIdAsync(It.Is<GetUserByIdRequest>(r => r.Id == _idRequest.Id)))
                .ReturnsAsync((UserResponse)null);
        }

        [When(@"o cliente requisita o usuário por ID")]
        public async Task QuandoRequisitaUsuarioPorId()
        {
            _resultado = await _controller.GetById(_idRequest);
        }

        [Then(@"o sistema deve retornar OK com null")]
        public void EntaoRetornaOkComNull()
        {
            var ok = Assert.IsType<OkObjectResult>(_resultado);
            Assert.Null(ok.Value);
        }
    }
}
