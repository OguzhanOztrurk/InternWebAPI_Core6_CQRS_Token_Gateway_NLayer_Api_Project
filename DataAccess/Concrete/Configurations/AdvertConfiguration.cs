using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AdvertConfiguration:IEntityTypeConfiguration<Advert>
{
    public void Configure(EntityTypeBuilder<Advert> builder)
    {
        builder.HasKey(x => x.AdvertId);

        builder.Property(x => x.AdvertName).HasMaxLength(100);
        builder.Property(x => x.AdvertSummary).HasMaxLength(200);
    }
}