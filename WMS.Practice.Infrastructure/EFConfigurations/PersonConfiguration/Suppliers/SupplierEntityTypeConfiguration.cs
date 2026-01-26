namespace WMS.Practice.Infrastructure.EFConfigurations.PersonConfiguration
{
    public class SupplierEntityTypeConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(s => s.SupplierId);

            builder.Property(s => s.SupplierName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Address)
                   .IsRequired();
        }
    }
}
