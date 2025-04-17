namespace Ordering.Domain.Models.ValueObjects;

public record Payment
{
    public string CardNumber { get; init; } = default!;

    public string CardHolderName { get; init; } = default!;

    public string Expiration { get; init; } = default!;

    public string Cvv { get; init; } = default!;

    public int PaymentMethod { get; init; } = default!;
}
