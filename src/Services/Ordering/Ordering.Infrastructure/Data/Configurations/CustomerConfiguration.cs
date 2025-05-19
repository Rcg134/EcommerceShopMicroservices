using Ordering.Domain.Models.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                customerId => customerId.Value,  // 👈 how to store it in the DB
                dbId => CustomerId.Of(dbId));   // 👈 how to read it back into a value object

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        builder.Property(x => x.Email).HasMaxLength(255);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}

