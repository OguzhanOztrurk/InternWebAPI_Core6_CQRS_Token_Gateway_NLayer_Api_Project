using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class InternConfiguration:IEntityTypeConfiguration<Intern>
{
    public void Configure(EntityTypeBuilder<Intern> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.UserId);

        #endregion

        #region Columns

        builder.Property(x => x.Adress).HasMaxLength(300);

        #endregion

        #region Foreign Key

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Intern)
            .HasForeignKey<Intern>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}