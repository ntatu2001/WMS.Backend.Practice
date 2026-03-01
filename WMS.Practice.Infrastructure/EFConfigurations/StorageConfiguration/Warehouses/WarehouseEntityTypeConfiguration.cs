namespace WMS.Practice.Infrastructure.EFConfigurations.StorageConfiguration
{
    public class WarehouseEntityTypeConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            // Map to the "Warehouses" table
            builder.ToTable("Warehouses");

            // Configure primary key
            builder.HasKey(w => w.WarehouseId);

            // Configure WarehouseName property
            builder.Property(w => w.WarehouseName)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
