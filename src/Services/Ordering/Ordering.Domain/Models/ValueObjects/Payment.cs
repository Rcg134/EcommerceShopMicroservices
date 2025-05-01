namespace Ordering.Domain.Models.ValueObjects;

public record Payment
{
    public string CardNumber { get; init; } = default!;

    public string CardHolderName { get; init; } = default!;

    public string Expiration { get; init; } = default!;

    public string Cvv { get; init; } = default!;

    public int PaymentMethod { get; init; } = default!;

    //of method
    protected Payment()
    {
    }

    private Payment(string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        Expiration = expiration;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }


    public static Payment Of(string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
    {
        ArgumentNullException.ThrowIfNull(cardNumber);
        ArgumentNullException.ThrowIfNull(cardHolderName);
        ArgumentNullException.ThrowIfNull(expiration);
        ArgumentNullException.ThrowIfNull(cvv);
        ArgumentOutOfRangeException.ThrowIfNotEqual(cvv.Length, 3);

        return new Payment(cardNumber, cardHolderName, expiration, cvv, paymentMethod);
    }
}
