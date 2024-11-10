using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Contatos.Infra.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CompilacaoRepository : IRepository<Compilacao>
    {
        private readonly IDbConnection _dbConnection;

        public CompilacaoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateAsync(Compilacao compilacao, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();            
            parameters.Add("@data", compilacao.Data, DbType.DateTime, ParameterDirection.Input);
          
            await _dbConnection.ExecuteAsync("insert into compilacao (data) values (@data) ", parameters);
        }

        public async Task UpdateAsync(Compilacao compilacao, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@id", compilacao.id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@data", compilacao.Data, DbType.DateTime, ParameterDirection.Input);
            await _dbConnection.ExecuteAsync("UPDATE compilacao SET data=@data WHERE id=@id", parameters);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            await _dbConnection.ExecuteAsync("delete from compilacao where id=@id", parameters);
        }

        public async Task<IEnumerable<Compilacao>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var regioes = await _dbConnection.QueryAsync<Compilacao>("select * from compilacao ");
            return regioes;
        }

        public async Task<Compilacao> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            var compilacao = await _dbConnection.QueryFirstOrDefaultAsync<Compilacao>("select * from compilacao where id=@id", parameters);
            return compilacao;
        }
    }
}
