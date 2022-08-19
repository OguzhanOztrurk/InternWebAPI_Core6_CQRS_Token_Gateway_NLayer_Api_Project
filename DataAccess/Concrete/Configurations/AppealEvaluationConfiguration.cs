using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AppealEvaluationConfiguration:IEntityTypeConfiguration<AppealEvaluation>
{
    public void Configure(EntityTypeBuilder<AppealEvaluation> builder)
    {
        builder.HasKey(x => x.AppealId);

        builder.Property(x => x.Conclusion).HasMaxLength(50);
        builder.Property(x => x.ConclusionDetail).HasMaxLength(250);

        builder
            .HasOne(x => x.Appeal)
            .WithOne(x => x.AppealEvaluation)
            .HasForeignKey<AppealEvaluation>(x => x.AppealId)
            .OnDelete(DeleteBehavior.Restrict);
            
    }
}