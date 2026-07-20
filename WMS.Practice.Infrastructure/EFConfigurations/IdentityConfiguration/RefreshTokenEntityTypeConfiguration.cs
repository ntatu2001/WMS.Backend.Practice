namespace WMS.Practice.Infrastructure.EFConfigurations.IdentityConfiguration
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.TokenHash)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.HasIndex(t => t.TokenHash).IsUnique();
            builder.HasIndex(t => t.UserId);

            builder.Ignore(t => t.IsActive);

            builder.HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
