using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AdvertDetailConfiguration:IEntityTypeConfiguration<AdvertDetail>
{
    public void Configure(EntityTypeBuilder<AdvertDetail> builder)
    {
        builder.HasKey(x => x.AdvertId);

        builder.Property(x => x.CompanyInfo).HasMaxLength(400);
        builder.Property(x => x.WorkDefinition).HasMaxLength(250);
        builder.Property(x => x.Quality).HasMaxLength(400);
        builder.Property(x => x.WorkEnvironment).HasMaxLength(400);
        builder.Property(x => x.WorkHour).HasMaxLength(200);
        builder.Property(x => x.Facilities).HasMaxLength(400);
        builder.Property(x => x.Wage).HasMaxLength(16);
        
        builder
            .HasOne(x => x.Advert)
            .WithOne(x => x.AdvertDetail)
            .HasForeignKey<AdvertDetail>(x => x.AdvertId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}