namespace WMS.Practice.Infrastructure.EFConfigurations.StorageConfiguration
{
    public class LocationPropertyEntityTypeConfiguration : IEntityTypeConfiguration<LocationProperty>
    {
        public void Configure(EntityTypeBuilder<LocationProperty> builder)
        {
            // Map to the "LocationProperties" table
            builder.ToTable("LocationProperties");

            // Configure the primary key
            builder.HasKey(x => x.PropertyId);

            builder.Property(e => e.PropertyName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.PropertyValue)
                   .IsRequired()
                   .HasMaxLength(200);

            // Configure UnitOfMeasure with conversion to string
            builder.Property(x => x.UnitOfMeasure)
                   .HasConversion(x => x.ToString(), x => (UnitOfMeasure)Enum.Parse(typeof(UnitOfMeasure), x))
                   .IsRequired()
                   .HasMaxLength(50);

            // Configure the relationship between LocationProperty and Location
            builder.HasOne(x => x.Location)
                   .WithMany(x => x.Properties)
                   .HasForeignKey(x => x.LocationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
