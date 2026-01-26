namespace WMS.Practice.Infrastructure.EFConfigurations.PersonConfiguration
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.EmployeeName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(e => e.EmployeeClass)
                   .WithMany(e => e.Employees)
                   .HasForeignKey(e => e.EmployeeClassId)
                   .IsRequired();
        }
    }
}
