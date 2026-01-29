namespace WMS.Practice.Infrastructure.EFConfigurations.MaterialConfiguration
{
    public class MaterialSubLotEntityTypeConfiguration : IEntityTypeConfiguration<MaterialSubLot>
    {
        public void Configure(EntityTypeBuilder<MaterialSubLot> builder)
        {
            builder.ToTable("MaterialSubLots");

            builder.HasKey(msl => msl.MaterialSubLotId);

            builder.Property(e => e.SubLotStatus)
                   .HasConversion(x => x.ToString(), x => (LotStatus)Enum.Parse(typeof(LotStatus), x));

            builder.HasOne(msl => msl.Location)
                   .WithMany(x => x.MaterialSubLots)
                   .HasForeignKey(x => x.LocationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.MaterialLot)
                   .WithMany(ml => ml.SubLots)
                   .HasForeignKey(ml => ml.LotNumber)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
