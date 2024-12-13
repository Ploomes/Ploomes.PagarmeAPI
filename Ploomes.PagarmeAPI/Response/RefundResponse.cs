using System.Text.Json.Serialization;

public class RefundResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}
