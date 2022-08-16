using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AdvertConfiguration:IEntityTypeConfiguration<Advert>
{
    public void Configure(EntityTypeBuilder<Advert> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.AdvertId);

        #endregion

        #region Columns
        builder.Property(x => x.AdvertName).HasMaxLength(100);
        builder.Property(x => x.AdvertSummary).HasMaxLength(200);

        #endregion

        #region Foreign Key

        builder
            .HasOne(x => x.AdvertCategory)
            .WithMany(x => x.Adverts)
            .HasForeignKey(x=>x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Workplace)
            .WithMany(x => x.Adverts)
            .HasForeignKey(x => x.WorkplaceId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

    }
}