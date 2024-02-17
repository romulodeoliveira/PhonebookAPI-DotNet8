/*
 * DOCUMENTAÇÃO LIDA PARA ESTE PROJETO
 *
 * Tutorial: Criar uma API mínima com o ASP.NET Core - MICROSOFT
 * https://learn.microsoft.com/pt-br/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio
 *
 * ASP.NET Minimal APIs - Por André Baltieri
 * https://balta.io/blog/aspnet-minimal-apis
 *
 * Novidades do .NET 7: Route Groups para organização de endpoints em Minimal APIs - Por Renato Groffe
 * https://renatogroffe.medium.com/novidades-do-net-7-route-groups-para-organiza%C3%A7%C3%A3o-de-endpoints-em-minimal-apis-c78c64b49dfa
 *
 * Simplifying Routes in .NET Core with Route Grouping: A Minimal API Approach - Por Sheldon Cohen
 * https://sheldonrcohen.medium.com/simplifying-routes-in-net-core-with-route-grouping-a-minimal-api-approach-b98ec8e53cd9
 * 
 * Minimal APIs quick reference - MICROSOFT
 * https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Phonebook.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// >>> Documentação da API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "API Agenda Telefonica",
            Description = "Exemplo de implementação de Minimal API para Agenda Telefonica", 
            Version = "v1",
            Contact = new OpenApiContact()
            {
                Name = "Romulo de Oliveira",
                Email = "dev@romulodeoliveira.net",
                Url = new Uri("https://github.com/romulodeoliveira"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        });
});

// >>> Configuração do Banco de Dados
string dbConfig = "Data Source=data.db;";
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(dbConfig));

// >>> Configuração de solicitações HTTP
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Phonebook v1");
    });
}

// >>> Configuração de solicitações HTTP
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// >>> Registrando os endpoints
var contactEndpoints = new Phonebook.Endpoints.Contact();
contactEndpoints.RegisterEndpoints(app);

var userEndpoints = new Phonebook.Endpoints.User();
userEndpoints.RegisterEndpoints(app);

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
