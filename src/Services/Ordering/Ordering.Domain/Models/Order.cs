﻿namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    private List<OrderItem> _orderItems { get; set; }

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Guid CustomerId { get; private set; } = default!;

    public string OrderName { get; private set; } = default!;

    public Address ShippingAddress { get; private set; } = default!;

    public string BillingAddress { get; private set; } = default!;

    public Payment payment { get; private set; } = default!;

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);

        private set { }
    }
}
