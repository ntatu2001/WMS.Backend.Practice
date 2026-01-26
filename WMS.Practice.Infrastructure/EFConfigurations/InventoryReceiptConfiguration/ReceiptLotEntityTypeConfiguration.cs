namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryReceiptConfiguration
{
    public class ReceiptLotEntityTypeConfiguration : IEntityTypeConfiguration<ReceiptLot>
    {
        public void Configure(EntityTypeBuilder<ReceiptLot> builder)
        {
            builder.ToTable("ReceiptLots");

            builder.HasKey(e => e.ReceiptLotId);

            builder.Property(e => e.LotStatus)
                   .HasConversion(x => x.ToString(), x => (LotStatus)Enum.Parse(typeof(LotStatus), x));

            builder.Property(e => e.ImportedQuantity)
                   .IsRequired();

            builder.Ignore(e => e.Material);

            // Configure one-to-many relationship with InventoryReceipt
            builder.HasOne(e => e.InventoryReceiptEntry)
                   .WithOne(e => e.ReceiptLot)
                   .HasForeignKey<ReceiptLot>(e => e.InventoryReceiptEntryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
