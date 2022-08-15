using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AppealConfiguration:IEntityTypeConfiguration<Appeal>
{
    public void Configure(EntityTypeBuilder<Appeal> builder)
    {
        builder.HasKey(x => x.AppealId);
        
        
    }
}