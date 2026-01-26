namespace WMS.Practice.Infrastructure.EFConfigurations.StorageConfiguration
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            // Map to the "Locations" table
            builder.ToTable("Locations");

            // Configure the primary key
            builder.HasKey(x => x.LocationCode);

            // Configure properties with constraints and column mappings
            builder.HasOne(x => x.Warehouse)
                   .WithMany(y => y.Locations)
                   .HasForeignKey(x => x.WarehouseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
