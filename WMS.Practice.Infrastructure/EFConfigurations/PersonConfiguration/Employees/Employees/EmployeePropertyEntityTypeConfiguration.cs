namespace WMS.Practice.Infrastructure.EFConfigurations.PersonConfiguration
{
    public class EmployeePropertyEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeProperty>
    {
        public void Configure(EntityTypeBuilder<EmployeeProperty> builder)
        {
            builder.ToTable("EmployeeProperties");

            builder.HasKey(e => e.PropertyId);

            builder.Property(e => e.PropertyName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.PropertyValue)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(x => x.Employee)
                   .WithMany(e => e.Properties)
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
