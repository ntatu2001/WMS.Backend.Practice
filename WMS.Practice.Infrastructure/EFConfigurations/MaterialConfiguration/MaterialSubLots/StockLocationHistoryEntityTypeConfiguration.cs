namespace WMS.Practice.Infrastructure.EFConfigurations.MaterialConfiguration
{
    public class StockLocationHistoryEntityTypeConfiguration : IEntityTypeConfiguration<StockLocationHistory>
    {
        public void Configure(EntityTypeBuilder<StockLocationHistory> builder)
        {
            builder.ToTable("StockLocationHistories");

            builder.HasKey(e => e.StockLocationHistoryId);

            builder.Property(e => e.Quantity)
                   .IsRequired();

            builder.Property(e => e.MovementType)
                   .HasConversion(x => x.ToString(), x => (StockMovementType)Enum.Parse(typeof(StockMovementType), x))
                   .IsRequired();

            builder.Property(e => e.EventDate)
                   .IsRequired();

            builder.HasOne(e => e.Location)
                   .WithMany(x => x.StockLocationHistories)
                   .HasForeignKey(e => e.LocationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
