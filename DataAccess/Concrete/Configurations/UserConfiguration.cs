using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.UserId);

        #endregion

        #region Columns

        builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Number).HasMaxLength(11);
        builder.Property(x => x.Email).HasMaxLength(150);

        #endregion
    }
}