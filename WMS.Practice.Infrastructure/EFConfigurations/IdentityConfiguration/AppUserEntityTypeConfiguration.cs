namespace WMS.Practice.Infrastructure.EFConfigurations.IdentityConfiguration
{
    public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.EmployeeId)
                   .HasMaxLength(450);

            builder.HasOne(u => u.Employee)
                   .WithOne()
                   .HasForeignKey<AppUser>(u => u.EmployeeId)
                   .HasPrincipalKey<Employee>(e => e.EmployeeId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
