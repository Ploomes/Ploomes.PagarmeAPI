namespace PagarmeCore.Models;

public class Plan
{
    private string _id;
    private string _name;

    public string Id
    {
        get => _id;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Plan ID cannot be empty.");
            _id = value;
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Plan name is required.");
            _name = value;
        }
    }

    public int Amount { get; set; }
}
