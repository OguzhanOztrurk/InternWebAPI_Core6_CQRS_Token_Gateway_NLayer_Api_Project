using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AppealConfiguration:IEntityTypeConfiguration<Appeal>
{
    public void Configure(EntityTypeBuilder<Appeal> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.AppealId);

        #endregion

        #region Foreign Key

        builder
            .HasOne(x => x.Advert)
            .WithMany(x => x.Appeals)
            .HasForeignKey(x => x.AdvertId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

    }
}