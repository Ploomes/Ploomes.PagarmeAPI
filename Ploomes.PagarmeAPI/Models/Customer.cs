namespace PagarmeCore.Models;

public class Customer
{
    private string _name;
    private string _email;
    private string _documentNumber;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name is required.");
            _name = value;
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                throw new ArgumentException("E-mail invalid.");
            _email = value;
        }
    }

    public string DocumentNumber
    {
        get => _documentNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 11 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid document. Must contain at least 11 digits.");
            _documentNumber = value;
        }
    }

    public Address Address { get; set; }
    public Phone Phone { get; set; }
}