namespace WMS.Practice.Infrastructure.EFConfigurations.MaterialConfiguration
{
    public class MaterialEntityTypeConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");

            builder.HasKey(m => m.MaterialId);

            builder.Property(m => m.MaterialName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(m => m.MaterialClass)
                   .WithMany(m => m.Materials)
                   .HasForeignKey(m => m.MaterialClassId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
