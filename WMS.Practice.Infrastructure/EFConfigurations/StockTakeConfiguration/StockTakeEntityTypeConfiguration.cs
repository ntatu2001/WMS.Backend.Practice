namespace WMS.Practice.Infrastructure.EFConfigurations.StockTakeConfiguration
{
    public class StockTakeEntityTypeConfiguration : IEntityTypeConfiguration<StockTake>
    {
        public void Configure(EntityTypeBuilder<StockTake> builder)
        {
            builder.ToTable("StockTakes");

            builder.HasKey(e => e.StockTakeId);

            builder.Property(e => e.AdjustmentDate)
                   .IsRequired();

            builder.Property(e => e.PreviousQuantity)
                   .IsRequired();

            builder.Property(e => e.AdjustedQuantity)
                   .IsRequired();

            builder.Property(e => e.Reason)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<AdjustmentReason>(x))
                   .IsRequired();

            builder.Property(e => e.Type)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<AdjustmentType>(x))
                   .IsRequired();

            builder.Property(e => e.Status)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<AdjustmentStatus>(x))
                   .IsRequired();

            // One-to-many: MaterialLot -> StockTakes
            builder.HasOne(e => e.MaterialLot)
                   .WithMany(m => m.StockTakes)
                   .HasForeignKey(e => e.LotNumber)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-many: Employee -> StockTakes
            builder.HasOne(e => e.Employee)
                   .WithMany(emp => emp.StockTakes)
                   .HasForeignKey(e => e.EmployeeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-many: Warehouse -> StockTakes
            builder.HasOne(e => e.Warehouse)
                   .WithMany(w => w.StockTakes)
                   .HasForeignKey(e => e.WarehouseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
