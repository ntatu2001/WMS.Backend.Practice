namespace WMS.Practice.Infrastructure.EFConfigurations.PersonConfiguration
{
    public class EmployeeClassEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeClass>
    {
        public void Configure(EntityTypeBuilder<EmployeeClass> builder)
        {
            // Map to "EmployeeClasses" table
            builder.ToTable("EmployeeClasses");

            // Configure primary key
            builder.HasKey(ec => ec.EmployeeClassId);

            // Configure EmployeeClassName property
            builder.Property(ec => ec.EmployeeClassName)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
