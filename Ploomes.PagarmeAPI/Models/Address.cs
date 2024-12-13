namespace PagarmeCore.Models;
public class Address
{
    private string _street;
    private string _city;
    private string _state;
    private string _zipCode;

    public string Street
    {
        get => _street;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Street is mandatory.");
            _street = value;
        }
    }

    public string Number { get; set; }

    public string City
    {
        get => _city;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("City is mandatory.");
            _city = value;
        }
    }

    public string State
    {
        get => _state;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 2)
                throw new ArgumentException("State must be a 2 letter acronym.");
            _state = value.ToUpper();
        }
    }

    public string ZipCode
    {
        get => _zipCode;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 8 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid zip code. Must contain 8 numeric digits.");
            _zipCode = value;
        }
    }
}
