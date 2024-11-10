using Contatos.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Infra.Services.Interfaces
{
    public interface IContatoServices : IServices<Contato>
    {
        public Task<IEnumerable<Contato>> GetAllAsync(int? ddd, CancellationToken cancellationToken);
    }
}
