using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhDai.Db.Configuration;

internal class UserPasswordConfiguration : BaseModelConfiguration<UserPassword>
{
    public override void Configure(EntityTypeBuilder<UserPassword> builder)
    {
        Sequence = "SEQ_USER_PASSWOED_ID";
        builder.HasKey(e => e.Id).HasName("PK_USER_PASSWORD_ID");
        builder.ToTable("USER_PASSWORD");
        builder.Property(e => e.UserId).HasPrecision(10).HasColumnName("USER_ID");
        builder.Property(e => e.Password).HasMaxLength(128).IsUnicode(false).HasColumnName("PASSWORD");
        base.Configure(builder);
    }
}
