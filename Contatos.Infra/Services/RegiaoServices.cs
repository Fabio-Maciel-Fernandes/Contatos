using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services.Interfaces;

namespace Contatos.Infra.Services
{
    public class RegiaoServices : IServices<Regiao>
    {
        private readonly IRepository<Regiao> _repository;
        private readonly ICacheService _cacheServices;
        private readonly string CHAVE_CACHE_REGIAO = "CHAVE_CACHE_REGIAO";

        public RegiaoServices(IRepository<Regiao> repository, ICacheService cacheServices)
        {
            _repository = repository;
            _cacheServices = cacheServices;
        }

        public async Task CreateAsync(Regiao model, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(model, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<bool> ExistAsync(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken) != null;
        }

        public async Task<IEnumerable<Regiao>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _cacheServices.GetOrCreateAsync(CHAVE_CACHE_REGIAO,
                 () => _repository.GetAllAsync(cancellationToken));             
        }

        public async Task<Regiao> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _cacheServices.GetOrCreateAsync(CHAVE_CACHE_REGIAO + id,
                 () => _repository.GetByIdAsync(id, cancellationToken));           
        }

        public async Task UpdateAsync(Regiao model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
        }
    }
}