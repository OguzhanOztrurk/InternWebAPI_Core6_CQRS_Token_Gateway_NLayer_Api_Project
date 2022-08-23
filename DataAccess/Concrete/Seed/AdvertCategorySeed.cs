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
                CategoryName = "Yazılım",
                Categorydefinition = "Yazılım alanında staj ilanları eklenebilir ve görüntülenir."
            },
            new AdvertCategory
            {
                CategoryId = 2,
                CategoryName = "İnşşat Teknikeri",
                Categorydefinition = "İnşaat teknikeri alanında staj ilanları eklenebilir ve görüntülenir."
            },
        new AdvertCategory
            {
                CategoryId = 3,
                CategoryName = "Makine Mühendisliği",
                Categorydefinition = "Makine mühendisliği alanında staj ilanları eklenebilir ve görüntülenir."
            },
            new AdvertCategory
            {
                CategoryId = 4,
                CategoryName = "Çocuk gelişimi",
                Categorydefinition = "Çocuk gelişimi alanında staj ilanları eklenebilir ve görüntülenir."
            },
            new AdvertCategory
            {
                CategoryId = 5,
                CategoryName = "Harita Mühendisliği",
                Categorydefinition = "Harita mühendisliği alanında staj ilanları eklenebilir ve görüntülenir."
            }
        );
    }
}