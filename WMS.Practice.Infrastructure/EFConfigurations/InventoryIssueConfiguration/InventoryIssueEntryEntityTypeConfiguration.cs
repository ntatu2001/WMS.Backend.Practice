
namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryIssueConfiguration
{
    public class InventoryIssueEntryEntityTypeConfiguration : IEntityTypeConfiguration<InventoryIssueEntry>
    {
        public void Configure(EntityTypeBuilder<InventoryIssueEntry> builder)
        {
            builder.ToTable("InventoryIssueEntries");

            builder.HasKey(x => x.InventoryIssueEntryId);

            builder.Property(x => x.PurchaseOrderNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.RequestedQuantity)
                   .IsRequired();

            builder.Property(x => x.MaterialName)
                   .HasMaxLength(200);

            builder.HasOne(x => x.IssueLot)
                   .WithOne(x => x.InventoryIssueEntry)
                   .HasForeignKey<InventoryIssueEntry>(x => x.IssueLotId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.InventoryIssue)
                   .WithMany(x => x.Entries)
                   .HasForeignKey(x => x.InventoryIssueId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
