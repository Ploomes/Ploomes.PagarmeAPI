using System.Text.Json.Serialization;

namespace PagarmeCore.Response;

public class CaptureResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; set; }
}
