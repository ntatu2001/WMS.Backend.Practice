namespace WMS.Practice.Infrastructure.EFConfigurations.MaterialConfiguration
{
    public class MaterialLotPropertyEntityTypeConfiguration : IEntityTypeConfiguration<MaterialLotProperty>
    {
        public void Configure(EntityTypeBuilder<MaterialLotProperty> builder)
        {
            builder.ToTable("MaterialLotProperties");

            builder.HasKey(e => e.PropertyId);

            builder.Property(e => e.PropertyName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.PropertyValue)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(e => e.MaterialLot)
                .WithMany(e => e.Properties)
                .HasForeignKey(e => e.MaterialLotId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
