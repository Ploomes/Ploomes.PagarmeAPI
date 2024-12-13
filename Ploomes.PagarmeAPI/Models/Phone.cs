namespace PagarmeCore.Models;

public class Phone
{
    private string _ddd;
    private string _number;

    public string Ddd
    {
        get => _ddd;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 2 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid area code. Must contain 2 digits.");
            _ddd = value;
        }
    }

    public string Number
    {
        get => _number;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 8 || value.Length > 9 || !value.All(char.IsDigit))
                throw new ArgumentException("Invalid number. Must contain 8 or 9 digits.");
            _number = value;
        }
    }
}
