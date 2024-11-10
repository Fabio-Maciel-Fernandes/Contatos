using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Contatos.Infra.Services
{
    public class CompilacaoServices : IServices<Compilacao>
    {
        private readonly IRepository<Compilacao> _repository;
       
        public CompilacaoServices(IRepository<Compilacao> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Compilacao model, CancellationToken cancellationToken)
        {
            if (model.Ok())
            {
                await _repository.CreateAsync(model, cancellationToken);
            }
            else
            {
                throw new ValidationException(model.ErrrsString());
            }
            
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<bool> ExistAsync(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken) != null;
        }

        public async Task<IEnumerable<Compilacao>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Compilacao> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(Compilacao model, CancellationToken cancellationToken)
        {
            if (model.Ok())
            {
                await _repository.UpdateAsync(model, cancellationToken);
            }
            else
            {
                throw new ValidationException(model.ErrrsString());
            }

        }
    }
}