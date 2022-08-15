using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class WorkplaceInternConfiguration:IEntityTypeConfiguration<WorkplaceIntern>
{
    public void Configure(EntityTypeBuilder<WorkplaceIntern> builder)
    {
        builder.HasKey(x => x.WorkplaceInternId);
    }
}