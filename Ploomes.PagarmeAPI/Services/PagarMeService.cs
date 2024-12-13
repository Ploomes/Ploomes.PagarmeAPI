using PagarmeCore.Models;
using PagarmeCore.Request;
using PagarmeCore.Response;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PagarmeCore.Services;

public class PagarMeService
{
    private readonly HttpClient _httpClient;

    public PagarMeService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<CardHashKey> GetCardHashKeyAsync()
    {
        var response = await _httpClient.GetAsync("transactions/card_hash_key");
        Console.WriteLine($"Request URL: {_httpClient.BaseAddress}transactions/card_hash_key");
        Console.WriteLine($"Authorization Header: {_httpClient.DefaultRequestHeaders.Authorization}");

        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Body: {responseBody}");

        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
        };

        return JsonSerializer.Deserialize<CardHashKey>(responseBody, options);
    }

    public async Task<TransactionResponse> CreateTransactionAsync(CardRequest card, TransactionRequest transaction)
    {
        var payload = new TransactionPayload
        {
            Card = card,
            Transaction = transaction
        };

        var response = await _httpClient.PostAsJsonAsync("transactions", payload);

        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Status Code: {response.StatusCode}");
        Console.WriteLine($"Response Body: {responseBody}");

        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<TransactionResponse>(responseBody);
    }


    public async Task<CaptureResponse> CaptureTransactionAsync(string id)
    {
        var response = await _httpClient.PostAsync($"transactions/{id}/capture", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CaptureResponse>();
    }

    public async Task<RefundResponse> RefundTransactionAsync(string id)
    {
        var response = await _httpClient.PostAsync($"transactions/{id}/refund", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RefundResponse>();
    }

    public async Task<string> GenerateCardHashAsync(Card card)
    {
        var cardHashKey = await GetCardHashKeyAsync();

        if (string.IsNullOrWhiteSpace(cardHashKey.PublicKey))
        {
            throw new InvalidOperationException("The public key returned by the API is null or invalid.");
        }

        using var rsa = RSA.Create();
        rsa.ImportFromPem(cardHashKey.PublicKey.AsSpan());

        var cardData = $"card_number={card.CardNumber}&card_holder_name={card.CardHolderName}&card_expiration_date={card.CardExpirationDate}&card_cvv={card.CardCvv}";
        var encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(cardData), RSAEncryptionPadding.Pkcs1);

        return $"{cardHashKey.Id}_{Convert.ToBase64String(encryptedData)}";
    }
}
