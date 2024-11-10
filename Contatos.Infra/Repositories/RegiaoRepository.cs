using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Contatos.Infra.Repositories
{
    [ExcludeFromCodeCoverage]
    public class RegiaoRepository : IRepository<Regiao>
    {
        private readonly IDbConnection _dbConnection;

        public RegiaoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateAsync(Regiao regiao, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@ddd", regiao.DDD, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@nome", regiao.Nome, DbType.String, ParameterDirection.Input);
            parameters.Add("@estado", regiao.Estado, DbType.String, ParameterDirection.Input);
            await _dbConnection.ExecuteAsync("insert into regiao (ddd, nome, estado) values (@ddd, @nome, @estado) ", parameters);
        }

        public async Task UpdateAsync(Regiao regiao, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@ddd", regiao.DDD, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@nome", regiao.Nome, DbType.String, ParameterDirection.Input);
            parameters.Add("@estado", regiao.Estado, DbType.String, ParameterDirection.Input);
            await _dbConnection.ExecuteAsync("UPDATE regiao SET nome=@nome, estado=@estado WHERE ddd=@ddd", parameters);
        }

        public async Task DeleteAsync(int ddd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@ddd", ddd, DbType.Int32, ParameterDirection.Input);
            await _dbConnection.ExecuteAsync("delete from regiao where ddd=@ddd", parameters);
        }

        public async Task<IEnumerable<Regiao>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var regioes = await _dbConnection.QueryAsync<Regiao>("select * from regiao ");
            return regioes;
        }

        public async Task<Regiao> GetByIdAsync(int ddd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@ddd", ddd, DbType.Int32, ParameterDirection.Input);
            var regiao = await _dbConnection.QueryFirstOrDefaultAsync<Regiao>("select * from regiao where ddd=@ddd", parameters);
            return regiao;
        }
    }
}
