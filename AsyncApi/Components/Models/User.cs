namespace AsyncApi.Components.Models;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address Address { get; set; } = new Address();
}

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string Suite { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
}

