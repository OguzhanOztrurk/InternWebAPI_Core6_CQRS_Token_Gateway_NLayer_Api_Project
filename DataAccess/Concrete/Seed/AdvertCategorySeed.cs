using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Seed;

public class AdvertCategorySeed:IEntityTypeConfiguration<AdvertCategory>
{
    public void Configure(EntityTypeBuilder<AdvertCategory> builder)
    {
        builder.HasData(
            new AdvertCategory
            {
                CategoryId = 1,
                CategoryName = "Software",
                Categorydefinition = "Companies that provide services in the field of software."
            },
            new AdvertCategory
            {
                CategoryId = 2,
                CategoryName = "Architecture",
                Categorydefinition = "Companies serving in the field of architecture.."
            },
        new AdvertCategory
            {
                CategoryId = 3,
                CategoryName = "Automobile",
                Categorydefinition = "Companies serving in the field of automobile."
            },
            new AdvertCategory
            {
                CategoryId = 4,
                CategoryName = "Machine",
                Categorydefinition = "Companies serving in the field of machine."
            },
            new AdvertCategory
            {
                CategoryId = 5,
                CategoryName = "Build",
                Categorydefinition = "Companies serving in the field of build."
            }
        );
    }
}