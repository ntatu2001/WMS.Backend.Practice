namespace WMS.Practice.Infrastructure.EFConfigurations.InventoryIssueConfiguration
{
    public class InventoryIssueEntityTypeConfiguration : IEntityTypeConfiguration<InventoryIssue>
    {
        public void Configure(EntityTypeBuilder<InventoryIssue> builder)
        {
            builder.ToTable("InventoryIssues");

            builder.HasKey(x => x.InventoryIssueId);

            // Additional configurations can be added here as needed
            builder.Property(x => x.IssueDate)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<IssueStatus>(x));

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.InventoryIssues)
                   .HasForeignKey(x => x.CustomerId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.InventoryIssues)
                   .HasForeignKey(x => x.EmployeeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Warehouse)
                   .WithMany(x => x.InventoryIssues)
                   .HasForeignKey(x => x.WarehouseId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
