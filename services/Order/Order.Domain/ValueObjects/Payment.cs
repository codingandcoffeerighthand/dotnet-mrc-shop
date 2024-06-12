namespace Order.Domain.ValueObjects;

public sealed record Payment
{
    public string CardName { get; init; } = default!;
    public string CardNumber { get; init; } = default!;
    public string CardHolderName { get; init; } = default!;
    public string Expiration { get; init; } = default!;
    public string Cvv { get; init; } = default!;
    public int PaymentMethod { get; init; } = default!;

    private Payment() { }
    private Payment(string cardName, string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        Expiration = expiration;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardName, string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);

        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
        return new Payment(cardName, cardNumber, cardHolderName, expiration, cvv, paymentMethod);
    }
}