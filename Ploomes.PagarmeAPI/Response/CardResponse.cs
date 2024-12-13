using System.Text.Json.Serialization;

namespace Pagarme.Response;

public class CardResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("last_digits")]
    public string LastDigits { get; set; }

    [JsonPropertyName("card_holder_name")]
    public string CardHolderName { get; set; }

    [JsonPropertyName("card_expiration_date")]
    public string CardExpirationDate { get; set; }
}
