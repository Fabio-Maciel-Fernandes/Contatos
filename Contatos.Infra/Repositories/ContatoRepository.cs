using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Contatos.Infra.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ContatoRepository : IContatoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContatoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateAsync(Contato contato, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@ddd", contato.Regiao.DDD, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@nome", contato.Nome, DbType.String, ParameterDirection.Input);
            parameters.Add("@email", contato.Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@telefone", contato.Telefone, DbType.String, ParameterDirection.Input);
            await _dbConnection.ExecuteAsync("insert into contato (ddd, nome, email, telefone) values (@ddd, @nome, @email, @telefone) ", parameters);
        }

        public async Task UpdateAsync(Contato contato, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@id", contato.id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@ddd", contato.Regiao.DDD, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@nome", contato.Nome, DbType.String, ParameterDirection.Input);
            parameters.Add("@email", contato.Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@telefone", contato.Telefone, DbType.String, ParameterDirection.Input);
            await _dbConnection.ExecuteAsync("UPDATE contato SET ddd=@ddd, nome=@nome, email=@email, telefone=@telefone WHERE id=@id", parameters);
        }

        public async Task DeleteAsync(int ddd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@id", ddd, DbType.Int32, ParameterDirection.Input);
            await _dbConnection.ExecuteAsync("delete from contato where id=@id", parameters);
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await this.GetAllAsync(null, cancellationToken);
        }

        public async Task<Contato> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);

            var strbQuery = new StringBuilder();
            strbQuery.Append(@"
                select * from contato c
                inner join regiao r on r.ddd = c.ddd
                where id=@id");

            var result = await _dbConnection.QueryAsync<Contato, Regiao, Contato>
            (
                strbQuery.ToString(),
                param: parameters,
                map: (contato, regiao) =>
                {
                    contato.Regiao = regiao;
                    return contato;
                },
                splitOn: "id,ddd"
            );

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(int? ddd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var parameters = new DynamicParameters();
            var strbQuery = new StringBuilder();
            strbQuery.Append(@"
                select * from contato c
                inner join regiao r on r.ddd = c.ddd ");

            if (ddd is not null)
            {
                parameters.Add("@ddd", ddd.Value, DbType.Int32, ParameterDirection.Input);
                strbQuery.Append(" WHERE c.ddd=@ddd ");
            }

            return await _dbConnection.QueryAsync<Contato, Regiao, Contato>
            (
                strbQuery.ToString(),
                param: parameters,
                map: (contato, regiao) =>
                {
                    contato.Regiao = regiao;
                    return contato;
                },
                splitOn: "id,ddd"
            );
        }
    }
}
