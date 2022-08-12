using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class WorkHistoryConfiguration:IEntityTypeConfiguration<WorkHistory>
{
    public void Configure(EntityTypeBuilder<WorkHistory> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.WorkHistoryId);

        #endregion

        #region Columns

        builder.Property(x => x.WorkplaceName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.OperationTime).IsRequired().HasMaxLength(30);

        #endregion
    }
}