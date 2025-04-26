namespace Ordering.Domain.Models;

public class Product : Entity<OrderId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}
