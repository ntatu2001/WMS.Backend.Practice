namespace WMS.Practice.Infrastructure.EFConfigurations.StorageConfiguration
{
    public class WarehousePropertyEntityTypeConfiguration : IEntityTypeConfiguration<WarehouseProperty>
    {
        public void Configure(EntityTypeBuilder<WarehouseProperty> builder)
        {
            // Map to the "WarehouseProperties" table
            builder.ToTable("WarehouseProperties");

            // Configure primary key
            builder.HasKey(x => x.PropertyId);

            // Configure UnitOfMeasure with conversion to string
            builder.Property(x => x.UnitOfMeasure)
                   .HasConversion(x => x.ToString(), x => (UnitOfMeasure)Enum.Parse(typeof(UnitOfMeasure), x))
                   .IsRequired()
                   .HasMaxLength(50);

            // Configure relationship with Warehouse
            builder.HasOne(x => x.Warehouse)
                   .WithMany(x => x.Properties)
                   .HasForeignKey(x => x.WarehouseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
