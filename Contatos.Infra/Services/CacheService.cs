using Contatos.Infra.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Contatos.Infra.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrCreateAsync<T>(string chave, Func<Task<T>> funcao, TimeSpan? duracao = null)
        {
            if (!_cache.TryGetValue(chave, out T dados))
            {
                dados = await funcao();
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = duracao ?? TimeSpan.FromMinutes(60)
                };
                _cache.Set(chave, dados, cacheEntryOptions);
            }

            return dados;
        }
    }
}
