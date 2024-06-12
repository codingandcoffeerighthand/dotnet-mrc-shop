using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infras.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Domain.Models.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(orderId => orderId.Value, orderId => OrderId.Of(orderId));

        builder.Property(x => x.CustomerId).HasConversion(customerId => customerId.Value, customerId => CustomerId.Of(customerId));
        builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId);

        builder.HasMany<OrderItem>(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Domain.Models.Order.OrderName))
                .HasMaxLength(100).IsRequired();
            }
        );
        static void addressBuilderFn(ComplexPropertyBuilder<Address> addressBuilder)
        {
            addressBuilder.Property(n => n.FirstName).HasMaxLength(50).IsRequired();
            addressBuilder.Property(n => n.LastName).HasMaxLength(50).IsRequired();
            addressBuilder.Property(n => n.AddressNumber).HasMaxLength(250).IsRequired();
            addressBuilder.Property(n => n.Country).HasMaxLength(50).IsRequired();
            addressBuilder.Property(n => n.City).HasMaxLength(50).IsRequired();
            addressBuilder.Property(n => n.ZipCode).HasMaxLength(5).IsRequired();
            addressBuilder.Property(n => n.Email).HasMaxLength(50);
        }
        builder.ComplexProperty(
            o => o.ShippingAddress, addressBuilderFn);
        builder.ComplexProperty(
            o => o.BillingAddress, addressBuilderFn);
        builder.ComplexProperty(
            o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName).HasMaxLength(50).IsRequired();
                paymentBuilder.Property(p => p.CardNumber).HasMaxLength(24).IsRequired();
                paymentBuilder.Property(p => p.CardHolderName).HasMaxLength(50);
                paymentBuilder.Property(p => p.Cvv).HasMaxLength(3).IsRequired();
                paymentBuilder.Property(p => p.PaymentMethod).IsRequired();
            }
        );

        builder.Property(x => x.Status).HasDefaultValue(OrderStatus.Draft).HasConversion(s => s.ToString(), s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));
        builder.Property(x => x.TotalPrice);
    }
}