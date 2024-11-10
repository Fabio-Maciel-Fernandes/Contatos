using Contatos.Api.Controllers;
using Contatos.Core.Models;
using Contatos.Infra.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Contatos.Test.Api.Controllers
{
    public class ContatoControllerTest
    {
        private readonly Mock<IContatoServices> _contatoServiceMock;
        private readonly ContatoController _contatoController;

        public ContatoControllerTest()
        {
            _contatoServiceMock = new Mock<IContatoServices>();
            _contatoController = new ContatoController(_contatoServiceMock.Object);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarOkQuandoExistemContatos()
        {
            var contatos = new List<Contato> { new Contato(), new Contato() };
            _contatoServiceMock.Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(contatos);

            var resultado = await _contatoController.GetAllAsync(null, new CancellationToken());

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var retorno = Assert.IsType<List<Contato>>(okResult.Value);
            Assert.Equal(contatos, retorno);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarOkQuandoContatoExiste()
        {
            var contato = new Contato();
            _contatoServiceMock.Setup(service => service.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(contato);

            var resultado = await _contatoController.GetContato(1, new CancellationToken());

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var retorno = Assert.IsType<Contato>(okResult.Value);
            Assert.Equal(contato, retorno);
        }

        [Fact]
        public async Task CriarAsync_DeveRetornarOkQuandoContatoCriado()
        {
            var contato = new Contato();
            _contatoServiceMock.Setup(service => service.CreateAsync(contato, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var resultado = await _contatoController.Post(contato, new CancellationToken());

            Assert.IsType<OkResult>(resultado);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarNoContentQuandoContatoAtualizado()
        {
            var contato = new Contato();
            _contatoServiceMock.Setup(service => service.ExistAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _contatoServiceMock.Setup(service => service.UpdateAsync(contato, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var resultado = await _contatoController.Put(1, contato, new CancellationToken());

            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task DeletarAsync_DeveRetornarNoContentQuandoContatoDeletado()
        {
            _contatoServiceMock.Setup(service => service.ExistAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _contatoServiceMock.Setup(service => service.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var resultado = await _contatoController.Delete(1, new CancellationToken());

            Assert.IsType<NoContentResult>(resultado);
        }
    }
}
