namespace WMS.Practice.Infrastructure.EFConfigurations.MaterialConfiguration
{
    public class MaterialPropertyEntityTypeConfiguration : IEntityTypeConfiguration<MaterialProperty>
    {
        public void Configure(EntityTypeBuilder<MaterialProperty> builder)
        {
            builder.ToTable("MaterialProperties");

            builder.HasKey(x => x.PropertyId);

            builder.Property(x => x.PropertyName)
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

            builder.HasOne(x => x.Material)
                   .WithMany(x => x.Properties)
                   .HasForeignKey(x => x.MaterialId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
