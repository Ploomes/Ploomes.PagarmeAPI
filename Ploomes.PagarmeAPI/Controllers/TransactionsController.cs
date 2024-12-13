using Microsoft.AspNetCore.Mvc;
using PagarmeCore.Models;
using PagarmeCore.Request;
using PagarmeCore.Services;

namespace PagarmeCore.Controller;

public static class TransactionController
{
    public static void MapTransactionEndpoints(WebApplication app)
    {
        app.MapPost("/transactions", async (PagarMeService pagarMeService, [FromBody] TransactionPayload payload) =>
        {
            try
            {
                var response = await pagarMeService.CreateTransactionAsync(payload.Card, payload.Transaction);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao criar transação: {ex.Message}");
            }
        });


        app.MapPost("/transactions/{id}/capture", async (PagarMeService pagarMeService, string id) =>
        {
            try
            {
                var result = await pagarMeService.CaptureTransactionAsync(id);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao capturar transação: {ex.Message}");
            }
        });

        app.MapPost("/transactions/{id}/refund", async (PagarMeService pagarMeService, string id) =>
        {
            try
            {
                var result = await pagarMeService.RefundTransactionAsync(id);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao estornar transação: {ex.Message}");
            }
        });

        app.MapPost("/generate-card-hash", async (PagarMeService pagarMeService, Card card) =>
        {
            try
            {
                var cardHash = await pagarMeService.GenerateCardHashAsync(card);
                return Results.Ok(new { CardHash = cardHash });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao gerar card hash: {ex.Message}");
            }
        });

        app.MapPost("/generate-hash", async (PagarMeService pagarMeService, Card card) =>
        {
            try
            {
                var cardHash = await pagarMeService.GenerateCardHashAsync(card);

                return Results.Ok(new { CardHash = cardHash });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao gerar card hash: {ex.Message}");
            }
        });

        app.MapGet("/card-hash-key", async (PagarMeService pagarMeService) =>
        {
            try
            {
                var cardHashKey = await pagarMeService.GetCardHashKeyAsync();
                return Results.Ok(cardHashKey);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao obter card hash key: {ex.Message}");
            }
        });
    }
}
