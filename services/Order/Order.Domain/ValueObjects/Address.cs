namespace Order.Domain.ValueObjects;

public sealed record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string AddressNumber { get; } = default!;
    public string Street { get; } = default!;
    public string City { get; } = default!;
    public string Country { get; } = default!;
    public string? Email { get; } = default!;
    public string ZipCode { get; } = default!;
    private Address() { }
    private Address(string firstName, string lastName, string addressNumber, string street, string city, string country, string zipCode, string? email)
    {
        FirstName = firstName;
        LastName = lastName;
        AddressNumber = addressNumber;
        Street = street;
        City = city;
        Country = country;
        ZipCode = zipCode;
        Email = email;
    }
    public static Address Of(string firstName, string lastName, string addressNumber, string street, string city, string country, string zipCode, string? email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressNumber);
        return new Address(firstName, lastName, addressNumber, street, city, country, zipCode, email);
    }
}
