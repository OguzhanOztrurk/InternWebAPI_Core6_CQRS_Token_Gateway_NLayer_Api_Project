using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AdminConfiguration:IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.UserId);

        #endregion

        #region Columns

        builder.Property(x => x.Position).HasMaxLength(150);

        #endregion

        #region Foreign Key

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Admin)
            .HasForeignKey<Admin>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion



    }
}