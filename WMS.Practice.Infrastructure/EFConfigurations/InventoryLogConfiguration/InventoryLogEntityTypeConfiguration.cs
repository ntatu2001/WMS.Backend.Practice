namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryLogConfiguration
{
    public class InventoryLogEntityTypeConfiguration : IEntityTypeConfiguration<InventoryLog>
    {
        public void Configure(EntityTypeBuilder<InventoryLog> builder)
        {
            builder.ToTable("InventoryLogs");

            builder.HasKey(il => il.InventoryLogId);

            builder.Property(x => x.TransactionDate)
                   .IsRequired();

            builder.Property(x => x.PreviousQuantity)
                   .IsRequired();

            builder.Property(x => x.ChangedQuantity)
                   .IsRequired();

            builder.Property(x => x.AfterQuantity)
                   .IsRequired();

            builder.Property(x => x.TransactionType)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<TransactionType>(x))
                   .IsRequired();

            builder.HasOne(il => il.MaterialLot)
                   .WithMany(e => e.InventoryLogs)
                   .HasForeignKey(il => il.LotNumber)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(il => il.Warehouse)
                   .WithMany(ml => ml.InventoryLogs)
                   .HasForeignKey(il => il.WarehouseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
