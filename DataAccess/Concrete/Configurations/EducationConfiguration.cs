using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class EducationConfiguration:IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.EducationId);

        #endregion

        #region Columns

        builder.Property(x => x.SchoolName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.StartYear).IsRequired().HasMaxLength(4);
        builder.Property(x => x.EndYear).IsRequired().HasMaxLength(4);

        #endregion

        
    }
}