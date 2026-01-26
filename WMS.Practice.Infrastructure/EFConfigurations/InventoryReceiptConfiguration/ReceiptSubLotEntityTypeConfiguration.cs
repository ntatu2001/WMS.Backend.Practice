namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryReceiptConfiguration
{
    public class ReceiptSubLotEntityTypeConfiguration : IEntityTypeConfiguration<ReceiptSubLot>
    {
        public void Configure(EntityTypeBuilder<ReceiptSubLot> builder)
        {
            builder.ToTable("ReceiptSubLots");

            builder.HasKey(e => e.ReceiptSubLotId);

            builder.Property(e => e.ImportedQuantity)
                   .IsRequired();

            builder.Property(e => e.LotStatus)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<LotStatus>(x))
                   .IsRequired();

            builder.Property(e => e.UnitOfMeasure)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<UnitOfMeasure>(x))
                   .IsRequired();

            // Configure one-to-many relationship with InventoryReceipt
            builder.HasOne(e => e.ReceiptLot)
                   .WithMany(e => e.ReceiptSubLots)
                   .HasForeignKey(e => e.ReceiptLotId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Location)
                   .WithMany(e => e.ReceiptSubLots)
                   .HasForeignKey(e => e.LocationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
