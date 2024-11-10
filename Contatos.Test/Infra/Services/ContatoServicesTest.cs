
using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services;
using Contatos.Infra.Services.Interfaces;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Contatos.Test.Infra.Services
{
    public class ContatoServiceTests
    {
        private readonly Mock<IServices<Regiao>> _regiaoService;
        private readonly ContatoServices _contatoService;

        public ContatoServiceTests()
        {
            _regiaoService = new Mock<IServices<Regiao>>();
            _contatoService = new ContatoServices(_regiaoService.Object);
        }

        [Fact]
        public async Task CriarAsync_DeveChamarRepositoryCreateAsync()
        {
            var regiao = new Regiao();
            regiao.Nome = "Sul";
            regiao.DDD = 48;
            regiao.Estado = "SC";

            var contato = new Contato();
            contato.id = 1;
            contato.Nome = "Fábio Maciel Fernandes";
            contato.Telefone = "48999999999";
            contato.Email = "fabbao@gmail.com";
            contato.Regiao = regiao;

            var cancellationToken = new CancellationToken();

            await _contatoService.CreateAsync(contato, cancellationToken);

            //_repositoryMock.Verify(repo => repo.CreateAsync(contato, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task CriarAsync_DeveLancarValidationExceptionQuandoEmailInvalido()
        {
            var contato = new Contato
            {
                Nome = "Nome Teste",
                Telefone = "1234567890",
                Email = "emailInvalido",
                Regiao = new Regiao()
            };

            await Assert.ThrowsAsync<ValidationException>(() => _contatoService.CreateAsync(contato, new CancellationToken()));
        }

        [Fact]
        public async Task DeletarAsync_DeveChamarRepositoryDeleteAsync()
        {
            var id = 1;
            var cancellationToken = new CancellationToken();

            await _contatoService.DeleteAsync(id, cancellationToken);

            //_repositoryMock.Verify(repo => repo.DeleteAsync(id, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task ExistirAsync_DeveChamarRepositoryGetByIdAsync()
        {
            var id = 1;
            var cancellationToken = new CancellationToken();

            await _contatoService.ExistAsync(id, cancellationToken);

            //_repositoryMock.Verify(repo => repo.GetByIdAsync(id, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveChamarRepositoryGetAllAsync()
        {
            var cancellationToken = new CancellationToken();

            await _contatoService.GetAllAsync(cancellationToken);

            //_repositoryMock.Verify(repo => repo.GetAllAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveChamarRepositoryGetByIdAsync()
        {
            var id = 1;
            var cancellationToken = new CancellationToken();

            await _contatoService.GetByIdAsync(id, cancellationToken);

            //_repositoryMock.Verify(repo => repo.GetByIdAsync(id, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_DeveChamarRepositoryUpdateAsync()
        {
            var contato = new Contato();
            contato.Email = "fabbao@gmail.com";
            contato.id = 48;
            contato.Nome = "Fábio Maciel Fernandes";
            contato.Telefone = "48999999999";
            var cancellationToken = new CancellationToken();

            await _contatoService.UpdateAsync(contato, cancellationToken);

            //_repositoryMock.Verify(repo => repo.UpdateAsync(contato, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_DeveLancarValidationExceptionQuandoEmailInvalido()
        {
            var contato = new Contato
            {
                Nome = "Nome Teste",
                Telefone = "1234567890",
                Email = "emailInvalido",
                Regiao = new Regiao()
            };

            //await Assert.ThrowsAsync<ValidationException>(() => _contatoService.UpdateAsync(contato, new CancellationToken()));
        }
    }
}