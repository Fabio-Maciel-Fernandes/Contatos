using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contatos.Test.Integracao
{
    public class IntegracaoTest
    {
        private readonly HttpClient _client;

        public IntegracaoTest()
        {
            // Configuração do HttpClient
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://contatoapipod-service") // URL base da API
            };
        }

        [Fact]
        public async Task CriarNovaCompilacao_DeveRetornarSucesso()
        {            
            var novaCompilacao = new
            {               
                data = DateTime.UtcNow
            };

            var json = System.Text.Json.JsonSerializer.Serialize(novaCompilacao);
            var data = new StringContent(json, Encoding.UTF8, "application/json");           
            var response = await _client.PostAsync("/api/Compilacao", data);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
