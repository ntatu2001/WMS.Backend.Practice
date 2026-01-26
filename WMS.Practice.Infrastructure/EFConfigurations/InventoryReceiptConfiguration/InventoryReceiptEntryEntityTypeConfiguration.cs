namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryReceiptConfiguration
{
    public class InventoryReceiptEntryEntityTypeConfiguration : IEntityTypeConfiguration<InventoryReceiptEntry>
    {
        public void Configure(EntityTypeBuilder<InventoryReceiptEntry> builder)
        {
            builder.ToTable("InventoryReceiptEntries");

            builder.HasKey(e => e.InventoryReceiptEntryId);

            builder.Property(e => e.PurchaseOrderNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.ImportedQuantity)
                   .IsRequired();

            builder.Property(e => e.Note)
                   .HasMaxLength(500);

            builder.HasOne(e => e.InventoryReceipt)
                   .WithMany(ir => ir.Entries)
                   .HasForeignKey(e => e.InventoryReceiptId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ReceiptLot)
                   .WithOne(x => x.InventoryReceiptEntry)
                   .HasForeignKey<InventoryReceiptEntry>(s => s.LotNumber)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
