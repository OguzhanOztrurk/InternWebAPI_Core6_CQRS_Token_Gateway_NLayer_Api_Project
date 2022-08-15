using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AdvertCategoryConfiguration:IEntityTypeConfiguration<AdvertCategory>
{
    public void Configure(EntityTypeBuilder<AdvertCategory> builder)
    {
        builder.HasKey(x => x.CategoryId);

        builder.Property(x => x.CategoryName).HasMaxLength(100);
        builder.Property(x => x.Categorydefinition).HasMaxLength(250);
    }
}