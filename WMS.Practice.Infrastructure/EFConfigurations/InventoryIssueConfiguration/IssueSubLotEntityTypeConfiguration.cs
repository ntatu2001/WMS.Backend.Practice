namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryIssueConfiguration
{
    public class IssueSubLotEntityTypeConfiguration : IEntityTypeConfiguration<IssueSubLot>
    {
        public void Configure(EntityTypeBuilder<IssueSubLot> builder)
        {
            builder.ToTable("IssueSubLots");

            builder.HasKey(isl => isl.IssueSubLotId);

            builder.Property(isl => isl.RequestedQuantity)
                   .IsRequired();

            builder.HasOne(isl => isl.MaterialSubLot)
                   .WithMany(x => x.IssueSubLots)
                   .HasForeignKey(isl => isl.MaterialSubLotId)
                   .IsRequired();

            builder.HasOne(isl => isl.IssueLot)
                   .WithMany(il => il.SubLots)
                   .HasForeignKey(isl => isl.IssueLotId)
                   .IsRequired();
        }
    }
}
