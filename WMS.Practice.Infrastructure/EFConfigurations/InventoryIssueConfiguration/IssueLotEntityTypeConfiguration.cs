namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryIssueConfiguration
{
    public class IssueLotEntityTypeConfiguration : IEntityTypeConfiguration<IssueLot>
    {
        public void Configure(EntityTypeBuilder<IssueLot> builder)
        {
            builder.ToTable("IssueLots");

            builder.HasKey(x => x.IssueLotId);

            builder.Property(x => x.RequestedQuantity)
                   .IsRequired();

            builder.Property(x => x.LotStatus)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<LotStatus>(x))
                   .IsRequired();

            builder.Ignore(x => x.Material);

            builder.HasOne(x => x.MaterialLot)
                   .WithMany(x => x.IssueLots)
                   .HasForeignKey(x => x.MaterialLotId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.InventoryIssueEntry)
                   .WithOne(x => x.IssueLot)
                   .HasForeignKey<IssueLot>(x => x.InventoryIssueEntryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
