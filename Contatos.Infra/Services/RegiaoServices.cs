using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services.Interfaces;

namespace Contatos.Infra.Services
{
    public class RegiaoServices : IServices<Regiao>
    {
        private readonly IRepository<Regiao> _repository;

        public RegiaoServices(IRepository<Regiao> repository)
        {
            _repository = repository;
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
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Regiao> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(Regiao model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);    
        }
    }
}