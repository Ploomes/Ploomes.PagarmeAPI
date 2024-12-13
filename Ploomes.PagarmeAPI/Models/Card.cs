namespace PagarmeCore.Models;

public class Card
{
    private string _cardNumber;
    private string _cardExpirationDate;
    private string _cardHolderName;

    public string CardNumber
    {
        get => _cardNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 16 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid card number. Must contain 16 digits.");
            _cardNumber = value;
        }
    }

    public string LastDigits
    {
        get => _cardNumber?.Substring(_cardNumber.Length - 4);
    }

    public string CardHolderName
    {
        get => _cardHolderName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Holder name is required.");
            _cardHolderName = value;
        }
    }

    public string CardExpirationDate
    {
        get => _cardExpirationDate;
        set
        {
            if (!IsValidExpirationDate(value))
                throw new ArgumentException("Invalid expiration date. Must be in MMYY format.");
            _cardExpirationDate = value;
        }
    }

    private static bool IsValidExpirationDate(string expirationDate)
    {
        if (string.IsNullOrWhiteSpace(expirationDate) || expirationDate.Length != 4 || !expirationDate.All(char.IsDigit))
            return false;

        var month = int.Parse(expirationDate.Substring(0, 2));
        return month >= 1 && month <= 12;
    }
    public string CardCvv { get; set; }
}