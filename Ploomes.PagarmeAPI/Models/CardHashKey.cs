using System.Text.Json.Serialization;

public class CardHashKey
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("public_key")]
    public string PublicKey { get; set; }
}
