namespace WMS.Practice.Infrastructure.EFConfigurations.MaterialConfiguration
{
    public class MaterialClassEntityTypeConfiguration : IEntityTypeConfiguration<MaterialClass>
    {
        public void Configure(EntityTypeBuilder<MaterialClass> builder)
        {
            builder.ToTable("MaterialClasses");

            builder.HasKey(e => e.MaterialClassId);

            builder.Property(e => e.MaterialClassId)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
