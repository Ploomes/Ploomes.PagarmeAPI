using System.Text.Json.Serialization;

namespace PagarmeCore.Request;

public class CardRequest
{
    [JsonPropertyName("card_number")]
    public string CardNumber { get; set; }

    [JsonPropertyName("card_holder_name")]
    public string CardHolderName { get; set; }

    [JsonPropertyName("card_expiration_date")]
    public string CardExpirationDate { get; set; }

    [JsonPropertyName("card_cvv")]
    public string CardCvv { get; set; }
}
