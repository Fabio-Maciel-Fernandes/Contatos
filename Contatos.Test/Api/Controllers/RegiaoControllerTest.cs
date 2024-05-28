using Contatos.Api.Controllers;
using Contatos.Core.Models;
using Contatos.Infra.Services;
using Contatos.Infra.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Contatos.Test.Api.Controllers
{
    public class RegiaoControllerTest
    {
        private readonly Mock<IServices<Regiao>> _regiaoServiceMock;
        private readonly RegiaoController _regiaoController;

        public RegiaoControllerTest()
        {
            _regiaoServiceMock = new Mock<IServices<Regiao>>();
            _regiaoController = new RegiaoController(_regiaoServiceMock.Object);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarOkQuandoExistemRegioes()
        {
            var regioes = new List<Regiao> { new Regiao(), new Regiao() };
            _regiaoServiceMock.Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(regioes);

            var resultado = await _regiaoController.GetAllAsync(new CancellationToken());

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var retorno = Assert.IsType<List<Regiao>>(okResult.Value);
            Assert.Equal(regioes, retorno);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarOkQuandoRegiaoExiste()
        {
            var regiao = new Regiao();
            _regiaoServiceMock.Setup(service => service.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(regiao);

            var resultado = await _regiaoController.GetRegiao(1, new CancellationToken());

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var retorno = Assert.IsType<Regiao>(okResult.Value);
            Assert.Equal(regiao, retorno);
        }

        [Fact]
        public async Task CriarAsync_DeveRetornarOkQuandoRegiaoCriada()
        {
            var regiao = new Regiao();
            _regiaoServiceMock.Setup(service => service.CreateAsync(regiao, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var resultado = await _regiaoController.Post(regiao, new CancellationToken());

            Assert.IsType<OkResult>(resultado);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarNoContentQuandoRegiaoAtualizada()
        {
            var regiao = new Regiao();
            _regiaoServiceMock.Setup(service => service.ExistAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _regiaoServiceMock.Setup(service => service.UpdateAsync(regiao, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var resultado = await _regiaoController.Put(1, regiao, new CancellationToken());

            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task DeletarAsync_DeveRetornarNoContentQuandoRegiaoDeletada()
        {
            _regiaoServiceMock.Setup(service => service.ExistAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _regiaoServiceMock.Setup(service => service.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var resultado = await _regiaoController.Delete(1, new CancellationToken());

            Assert.IsType<NoContentResult>(resultado);
        }
    }
}
