namespace WMS.Practice.Infrastructure.EFConfigurations.PersonConfiguration
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Map to the "Customers" table
            builder.ToTable("Customers");

            // Configure primary key
            builder.HasKey(c => c.CustomerId);

            // Configure CustomerName and ContactDetails properties
            builder.Property(c => c.CustomerName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.ContactDetails)
                   .IsRequired();
        }
    }
}
