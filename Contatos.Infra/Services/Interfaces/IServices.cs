namespace Contatos.Infra.Services.Interfaces
{
    public interface IServices<T>
    {
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<bool> ExistAsync(int id, CancellationToken cancellationToken);
        public Task CreateAsync(T model, CancellationToken cancellationToken);
        public Task UpdateAsync(T model, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
