using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services.Interfaces;
using Contatos.Shared.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Contatos.Infra.Services
{
    public class ContatoServices : IContatoServices
    {
        private readonly IContatoRepository _repository;

        public ContatoServices(IContatoRepository repository)
        {
            _repository = repository;           
        }

        public async Task CreateAsync(Contato model, CancellationToken cancellationToken)
        {
            if (model.Ok())
            {
                await _repository.CreateAsync(model, cancellationToken);
            }
            else
            {
                throw new ValidationException(model.ObterErros());
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

        public async Task<IEnumerable<Contato>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(int? ddd, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(ddd, cancellationToken);
        }

        public async Task<Contato> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(Contato model, CancellationToken cancellationToken)
        {
            if (model.Ok())
            {
                await _repository.UpdateAsync(model, cancellationToken);
            }
            else
            {
                throw new ValidationException(model.ObterErros());
            }              
        }
    }
}