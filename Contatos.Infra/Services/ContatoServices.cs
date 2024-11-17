using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services.Interfaces;
using Contatos.Shared.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using RabbitMQ.Client;
using System.Text;
using Contatos.Shared;
using System.Reflection;

namespace Contatos.Infra.Services
{
    public class ContatoServices : IContatoServices
    {
        private readonly IServices<Regiao> _regiaoService;
        private readonly HttpClient _client;
        private readonly ConnectionFactory factory;
        public ContatoServices(IServices<Regiao> regiaoService)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://mscontatoapipod-service") // URL base da API
            };
            _regiaoService = regiaoService;

            this.factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://nzosfmoq:uZNH48guR3ZZzmTWib0KoeZUYFICSQI7@fly.rmq.cloudamqp.com/nzosfmoq")
            };
        }

        public async Task CreateAsync(Contato model, CancellationToken cancellationToken)
        {
            if (model.Ok())
            {
                Publish(model, EnumOperacao.Inclusao);
            }
            else
            {
                throw new ValidationException(model.ObterErros());
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Publish(new Contato() { id = id }, EnumOperacao.Delete);
        }

        public async Task<bool> ExistAsync(int id, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(id, cancellationToken) != null;
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"/api/Contato/GetAllAsync");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Contato> contatos = JsonSerializer.Deserialize<List<Contato>>(responseBody, options);
                contatos.ForEach(contato =>
                {
                    contato.Regiao = _regiaoService.GetByIdAsync(contato.Regiao.DDD, cancellationToken).Result;
                });
                return contatos;
            }
            return default;
        }

        public async Task<IEnumerable<Contato>> GetAllAsync(int? ddd, CancellationToken cancellationToken)
        {        
            Console.WriteLine(_client.BaseAddress);
            var response = await _client.GetAsync($"/api/Contato/GetAllAsync?ddd={ddd}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Contato> contatos = JsonSerializer.Deserialize<List<Contato>>(responseBody, options);
                contatos.ForEach(contato =>
                {
                    contato.Regiao = _regiaoService.GetByIdAsync(contato.Regiao.DDD, cancellationToken).Result;
                });
                return contatos;
            }
            return default;
        }

        public async Task<Contato> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync($"/api/Contato/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                // Deserialize the JSON into the C# object
                Contato contato = JsonSerializer.Deserialize<Contato>(responseBody, options);
                contato.Regiao = await _regiaoService.GetByIdAsync(contato.Regiao.DDD, cancellationToken);
                return contato;
            }
            return default;
        }

        public async Task UpdateAsync(Contato model, CancellationToken cancellationToken)
        {
            if (model.Ok())
            {
                Publish(model, EnumOperacao.Update);
            }
            else
            {
                throw new ValidationException(model.ObterErros());
            }
        }

        public void Publish(Contato contato, EnumOperacao operacao)
        {
            string queue = string.Empty;
            switch (operacao)
            {
                case EnumOperacao.Inclusao:
                    queue = "CONTATO_INCLUSAO";
                    break;
                case EnumOperacao.Update:
                    queue = "CONTATO_UPDATE";
                    break;
                case EnumOperacao.Delete:
                    queue = "CONTATO_DELETE";
                    break;
                default:
                    break;
            }

            using (var connect = factory.CreateConnection())
            {
                using (var channel = connect.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false);
                    if (operacao == EnumOperacao.Delete)
                    {
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: queue,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(contato.id))
                            );
                    }
                    else
                    {
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: queue,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(contato))
                            );
                    }
                }
            }
        }
    }
}
