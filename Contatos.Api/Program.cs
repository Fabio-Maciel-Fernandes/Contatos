using Contatos.Api.Middlewares;
using Contatos.Core.Models;
using Contatos.Infra.Repositories;
using Contatos.Infra.Repositories.Interfaces;
using Contatos.Infra.Services;
using Contatos.Infra.Services.Interfaces;
using Dapper;
using Npgsql;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights.AspNetCore;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IServices<Regiao>, RegiaoServices>();
        builder.Services.AddTransient<IContatoServices, ContatoServices>();
        builder.Services.AddTransient<IServices<Compilacao>, CompilacaoServices>();
        builder.Services.AddTransient<IRepository<Regiao>, RegiaoRepository>();
        builder.Services.AddTransient<IContatoRepository, ContatoRepository>();
        builder.Services.AddTransient<IRepository<Compilacao>, CompilacaoRepository>();
        builder.Services.AddExceptionHandler<ExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddMemoryCache();
        builder.Services.AddTransient<ICacheService, CacheService>();
        builder.Services.AddApplicationInsightsTelemetry();

        builder.Services.AddHealthChecks();


        DefaultTypeMap.MatchNamesWithUnderscores = true;

        var connectionString = configuration.GetValue<string>("ConnectionStringPostgres");
        builder.Services.AddScoped<IDbConnection>((connection) => new NpgsqlConnection(connectionString));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
            app.UseSwagger();
            app.UseSwaggerUI();
        //}

        app.MapHealthChecks("/health");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseExceptionHandler();

        app.Run();
    }    
}

