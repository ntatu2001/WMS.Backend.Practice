namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryReceiptConfiguration
{
    public class InventoryReceiptEntityTypeConfiguration : IEntityTypeConfiguration<InventoryReceipt>
    {
        public void Configure(EntityTypeBuilder<InventoryReceipt> builder)
        {
            builder.ToTable("InventoryReceipts");

            builder.HasKey(x => x.InventoryReceiptId);

            builder.Property(e => e.ReceiptDate)
                   .IsRequired();

            builder.Property(e => e.ReceiptStatus)
                   .HasConversion(x => x.ToString(), x => (ReceiptStatus)Enum.Parse(typeof(ReceiptStatus), x));

            builder.HasOne(x => x.Supplier)
                   .WithMany(x => x.InventoryReceipts)
                   .HasForeignKey(x => x.SupplierId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.InventoryReceipts)
                   .HasForeignKey(x => x.EmployeeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Warehouse)
                   .WithMany(x => x.InventoryReceipts)
                   .HasForeignKey(x => x.WarehouseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
