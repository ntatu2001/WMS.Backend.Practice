namespace WMS.Practice.Infrastructure.EFConfigurations.StockTakeConfiguration
{
    public class StockTakeSubLotEntityTypeConfiguration : IEntityTypeConfiguration<StockTakeSubLot>
    {
        public void Configure(EntityTypeBuilder<StockTakeSubLot> builder)
        {
            builder.ToTable("StockTakeSubLots");

            builder.HasKey(stsl => stsl.StockTakeSubLotId);

            builder.Property(x => x.PreviousQuantity)
                   .IsRequired();

            builder.Property(x => x.AdjustedQuantity)
                   .IsRequired();

            builder.Property(x => x.QuantityDifference)
                   .IsRequired();

            builder.HasOne(stsl => stsl.StockTake)
                   .WithMany(e => e.SubLots)
                   .HasForeignKey(stsl => stsl.StockTakeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
