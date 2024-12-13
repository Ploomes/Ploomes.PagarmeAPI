using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PagarmeCore.Services;
using PagarmeCore.Controller;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var pagarMeConfig = builder.Configuration.GetSection("PagarMe");
var environment = pagarMeConfig["Environment"];
var pagarMeApiKey = builder.Configuration["PagarMe:ApiKey"];

Console.WriteLine($"API-Key loaded:: {pagarMeApiKey}");

if (string.IsNullOrEmpty(pagarMeApiKey))
    throw new Exception("API-Key is not configured!");

builder.Services.AddSingleton(pagarMeApiKey);

builder.Services.AddHttpClient<PagarMeService>(client =>
{
    client.BaseAddress = new Uri("https://api.pagar.me/1/");
    var apiKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{pagarMeApiKey}:"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiKeyBase64);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PagarMe API",
        Description = "API for integration with PagarMe"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PagarMe API v1");
        c.RoutePrefix = string.Empty;

        c.ConfigObject.AdditionalItems["servers"] = new object[]
        {
            new { url = "https://api.pagar.me/1/", description = "API" }
        };
    });
}


TransactionController.MapTransactionEndpoints(app);

app.Run();