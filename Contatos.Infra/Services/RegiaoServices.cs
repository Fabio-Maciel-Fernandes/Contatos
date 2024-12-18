﻿using Contatos.Core.Models;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services.Interfaces;
using Contatos.Shared;
using RabbitMQ.Client;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Contatos.Infra.Services
{
    public class RegiaoServices : IServices<Regiao>
    {
        private HttpClient _client;
        private readonly ConnectionFactory factory;
        private string url = "http://msregiaoapipod-service.default.svc.cluster.local";

        public RegiaoServices()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(url) // URL base da API
                //BaseAddress = new Uri("http://msregiaoapipod-service.default.svc.cluster.local")
            };

            this.factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://nzosfmoq:uZNH48guR3ZZzmTWib0KoeZUYFICSQI7@fly.rmq.cloudamqp.com/nzosfmoq")
            };
        }

        public async Task CreateAsync(Regiao model, CancellationToken cancellationToken)
        {
            if (model.Ok())
            {
                Publish(model, EnumOperacao.Inclusao);
            }
            else
            {
                throw new ValidationException(model.ErrrsString());
            }           
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Publish(new Regiao { DDD = id }, EnumOperacao.Delete);
        }

        public async Task<bool> ExistAsync(int id, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(id, cancellationToken) != null;
        }

        public async Task<IEnumerable<Regiao>> GetAllAsync(CancellationToken cancellationToken)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(url) // URL base da API                
            };
            
            Console.WriteLine(_client.BaseAddress);
            var response = await _client.GetAsync("/api/Regiao/GetAllAsync");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.RequestMessage.ToString());
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                // Deserialize the JSON into the C# object
                return JsonSerializer.Deserialize<IEnumerable<Regiao>>(responseBody, options);
            }

            Console.WriteLine(response.StatusCode.ToString());
            Console.WriteLine(response.RequestMessage.ToString());
            return default;
        }

        public async Task<Regiao> GetByIdAsync(int ddd, CancellationToken cancellationToken)
        {
            
            _client = new HttpClient
            {
                BaseAddress = new Uri(url) // URL base da API                
            };
            Console.WriteLine(_client.BaseAddress);
            var response = await _client.GetAsync($"/api/Regiao/{ddd}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.RequestMessage.ToString());
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                // Deserialize the JSON into the C# object
                return JsonSerializer.Deserialize<Regiao>(responseBody, options);
            }
            Console.WriteLine(response.StatusCode.ToString());
            Console.WriteLine(response.RequestMessage.ToString());
            return default;
        }

        public async Task UpdateAsync(Regiao model, CancellationToken cancellationToken)
        {
            if (model.Ok())
            {
                Publish(model, EnumOperacao.Update);
            }
            else
            {
                throw new ValidationException(model.ErrrsString());
            }
        }

        public void Publish(Regiao regiao, EnumOperacao operacao)
        {
            string queue = string.Empty;
            switch (operacao)
            {
                case EnumOperacao.Inclusao:
                    queue = "REGIAO_INCLUSAO";
                    break;
                case EnumOperacao.Update:
                    queue = "REGIAO_UPDATE";
                    break;
                case EnumOperacao.Delete:
                    queue = "REGIAO_DELETE";
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
                            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(regiao.DDD))
                            );
                    }
                    else
                    {
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: queue,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(regiao))
                            );
                    }
                }
            }
        }
    }
}
