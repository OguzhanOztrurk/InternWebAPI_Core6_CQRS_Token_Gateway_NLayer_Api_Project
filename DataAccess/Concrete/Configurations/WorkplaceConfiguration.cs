using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class WorkplaceConfiguration:IEntityTypeConfiguration<Workplace>
{
    public void Configure(EntityTypeBuilder<Workplace> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.WorkplaceId);

        #endregion

        #region Columns

        builder.Property(x => x.WorkplaceName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.WorkplaceExplanation).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.EmployeesCount).IsRequired();
        builder.Property(x => x.Vision).IsRequired().HasMaxLength(600);
        builder.Property(x => x.Mission).IsRequired().HasMaxLength(600);
        

        #endregion
        #region Foreign Key

        builder
            .HasOne(x => x.Admin)
            .WithMany(x => x.Workplace)
            .HasForeignKey(x => x.AdminId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}