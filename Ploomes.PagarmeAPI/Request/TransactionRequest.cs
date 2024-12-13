using PagarmeCore.Models;
using System.Text.Json.Serialization;

namespace PagarmeCore.Request;

public class TransactionRequest
{
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("card_hash")]
    public string CardHash { get; set; }

    [JsonPropertyName("payment_method")]
    public string PaymentMethod { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; }
}
