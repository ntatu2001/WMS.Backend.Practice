namespace WMS.Practice.Infrastructure.EFConfigurations.MaterialConfiguration
{
    public class MaterialLotEntityTypeConfiguration : IEntityTypeConfiguration<MaterialLot>
    {
        public void Configure(EntityTypeBuilder<MaterialLot> builder)
        {
            builder.ToTable("MaterialLots");

            builder.HasKey(e => e.LotNumber);

            // Configure LotStatus enum to be stored as string
            builder.Property(x => x.LotStatus)
                   .HasConversion(x => x.ToString(), y => (LotStatus)Enum.Parse(typeof(LotStatus), y));

            // Configure one-to-many relationship with Material
            builder.HasOne(e => e.Material)
                   .WithMany(e => e.MaterialLots)
                   .HasForeignKey(e => e.MaterialId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
