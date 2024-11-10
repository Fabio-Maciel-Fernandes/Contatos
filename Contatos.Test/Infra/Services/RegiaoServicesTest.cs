using Contatos.Core.Models;
using Contatos.Infra.Repositories;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services;
using Contatos.Infra.Services.Interfaces;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Contatos.Test.Infra.Services
{

    public class RegiaoServicesTests
    {
        private readonly RegiaoServices _regiaoServices;

        public RegiaoServicesTests()
        {           
            _regiaoServices = new RegiaoServices();
        }

        [Fact]
        public async Task CriarAsyncOk()
        {
            var regiao = new Regiao();
            regiao.Nome = "Sul";
            regiao.DDD = 48;
            regiao.Estado = "SC";    
            var cancellationToken = new CancellationToken();
            if (regiao.Ok())
            {
                await _regiaoServices.CreateAsync(regiao, cancellationToken);
            }
            else
            {
                throw new ValidationException(regiao.ErrrsString());
            }            
        }

        [Fact]
        public async Task CriarAsyncException()
        {
            var regiao = new Regiao();
            regiao.Nome = "Sul";
            regiao.DDD = 48;
            regiao.Estado = "SC";   
            var cancellationToken = new CancellationToken();

            //_repositoryMock.Setup(repo => repo.CreateAsync(regiao, cancellationToken)).ThrowsAsync(new Exception());

            //await Assert.ThrowsAsync<Exception>(() => _regiaoServices.CreateAsync(regiao, cancellationToken));
        }

        [Fact]
        public async Task DeleteAsyncOk()
        {
            var id = 1;
            var cancellationToken = new CancellationToken();

            await _regiaoServices.DeleteAsync(id, cancellationToken);

            //_repositoryMock.Verify(repo => repo.DeleteAsync(id, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task DeleteAsyncException()
        {
            var id = 1;
            var cancellationToken = new CancellationToken();

            //_repositoryMock.Setup(repo => repo.DeleteAsync(id, cancellationToken)).ThrowsAsync(new Exception());

            //await Assert.ThrowsAsync<Exception>(() => _regiaoServices.DeleteAsync(id, cancellationToken));
        }

        [Fact]
        public async Task ExistAsyncOk()
        {
            var id = 1;
            var cancellationToken = new CancellationToken();

            //_repositoryMock.Setup(repo => repo.GetByIdAsync(id, cancellationToken)).ReturnsAsync(new Regiao());

            //var result = await _regiaoServices.ExistAsync(id, cancellationToken);

            //Assert.True(result);
        }
        [Fact]
        public async Task ExistAsyncFalse()
        {
            var id = 1;
            var cancellationToken = new CancellationToken();

            //_repositoryMock.Setup(repo => repo.GetByIdAsync(id, cancellationToken)).ReturnsAsync((Regiao)null);

           // var result = await _regiaoServices.ExistAsync(id, cancellationToken);

           // Assert.False(result);
        }
        [Fact]
        public async Task ExistAsyncException()
        {
            var id = 1;
            var cancellationToken = new CancellationToken();

            //_repositoryMock.Setup(repo => repo.GetByIdAsync(id, cancellationToken)).ThrowsAsync(new Exception());

            //await Assert.ThrowsAsync<Exception>(() => _regiaoServices.ExistAsync(id, cancellationToken));
        }
        
        [Fact]
        public async Task UpdateAsyncOk()
        {
            var regiao = new Regiao();
            regiao.DDD = 48;
            regiao.Nome = "Sul";
            regiao.Estado = "SC";
            
            var cancellationToken = new CancellationToken();

            //await _regiaoServices.UpdateAsync(regiao, cancellationToken);

            //_repositoryMock.Verify(repo => repo.UpdateAsync(regiao, cancellationToken), Times.Once);
        }
        [Fact]
        public async Task UpdateAsyncException()
        {
            var regiao = new Regiao();
            var cancellationToken = new CancellationToken();

            //_repositoryMock.Setup(repo => repo.UpdateAsync(regiao, cancellationToken)).ThrowsAsync(new Exception());

           // await Assert.ThrowsAsync<ValidationException>(() => _regiaoServices.UpdateAsync(regiao, cancellationToken));
        }
    }
}
