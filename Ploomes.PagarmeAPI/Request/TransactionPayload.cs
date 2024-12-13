using System.Text.Json.Serialization;

namespace PagarmeCore.Request;

public class TransactionPayload
{
    [JsonPropertyName("card")]
    public CardRequest Card { get; set; }

    [JsonPropertyName("transaction")]
    public TransactionRequest Transaction { get; set; }
}
