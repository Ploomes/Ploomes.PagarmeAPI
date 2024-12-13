namespace PagarmeCore.Models;

public class Transaction
{
    private int _amount;
    private string _paymentMethod;

    public int Amount
    {
        get => _amount;
        set
        {
            if (value <= 0) throw new ArgumentException("Transaction amount must be greater than zero.");
            _amount = value;
        }
    }

    public string PaymentMethod
    {
        get => _paymentMethod;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || (value != "credit_card" && value != "boleto"))
                throw new ArgumentException("Invalid payment method. Must be 'credit_card' or 'boleto'.");
            _paymentMethod = value;
        }
    }

    public Card Card { get; set; }
    public Customer Customer { get; set; }
}
