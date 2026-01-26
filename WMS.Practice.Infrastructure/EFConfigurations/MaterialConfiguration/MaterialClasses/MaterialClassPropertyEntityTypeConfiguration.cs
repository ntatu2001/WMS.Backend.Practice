namespace WMS.Practice.Infrastructure.EFConfigurations.MaterialConfiguration
{
    public class MaterialClassPropertyEntityTypeConfiguration : IEntityTypeConfiguration<MaterialClassProperty>
    {
        public void Configure(EntityTypeBuilder<MaterialClassProperty> builder)
        {
            builder.ToTable("MaterialClassProperties");

            builder.HasKey(e => e.PropertyId);

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

            builder.HasOne(e => e.MaterialClass)
                   .WithMany(mc => mc.Properties)
                   .HasForeignKey(e => e.MaterialClassId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
