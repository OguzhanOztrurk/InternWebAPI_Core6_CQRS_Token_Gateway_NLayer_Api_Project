using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class WorkplaceInternConfiguration:IEntityTypeConfiguration<WorkplaceIntern>
{
    public void Configure(EntityTypeBuilder<WorkplaceIntern> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.WorkplaceInternId);

        #endregion

        #region Foreign Key

        builder
            .HasOne(x => x.Workplace)
            .WithMany(x => x.WorkplaceInterns)
            .HasForeignKey(x => x.WorkplaceId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

    }
}