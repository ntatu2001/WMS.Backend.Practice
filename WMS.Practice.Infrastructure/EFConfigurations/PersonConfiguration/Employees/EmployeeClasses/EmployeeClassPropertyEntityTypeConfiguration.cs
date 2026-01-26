namespace WMS.Practice.Infrastructure.EFConfigurations.PersonConfiguration
{
    public class EmployeeClassPropertyEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeClassProperty>
    {
        public void Configure(EntityTypeBuilder<EmployeeClassProperty> builder)
        {
            builder.ToTable("EmployeeClassProperties");

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

            builder.HasOne(e => e.EmployeeClass)
                   .WithMany(e => e.Properties)
                   .HasForeignKey(e => e.EmployeeClassId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
