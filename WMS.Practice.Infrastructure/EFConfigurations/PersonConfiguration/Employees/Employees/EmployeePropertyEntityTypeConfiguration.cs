namespace WMS.Practice.Infrastructure.EFConfigurations.PersonConfiguration
{
    public class EmployeePropertyEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeProperty>
    {
        public void Configure(EntityTypeBuilder<EmployeeProperty> builder)
        {
            builder.ToTable("EmployeeProperties");

            builder.HasKey(e => e.PropertyId);

            // Configure UnitOfMeasure with conversion to string
            builder.Property(x => x.UnitOfMeasure)
                   .HasConversion(x => x.ToString(), x => (UnitOfMeasure)Enum.Parse(typeof(UnitOfMeasure), x))
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.PropertyName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.PropertyValue)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(e => e.Employee)
                   .WithMany(e => e.Properties)
                   .HasForeignKey(e => e.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
