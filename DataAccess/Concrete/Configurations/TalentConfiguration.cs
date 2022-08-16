using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class TalentConfiguration:IEntityTypeConfiguration<Talent>
{
    public void Configure(EntityTypeBuilder<Talent> builder)
    {
        #region Primary Key

        builder.HasKey(x => x.TalentId);

        #endregion

        #region Columns

        builder.Property(x => x.TalentName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.TalentExplanation).IsRequired().HasMaxLength(400);

        #endregion

        #region Foreign Key

        builder
            .HasOne(x => x.Intern)
            .WithMany(x => x.Talent)
            .HasForeignKey(x => x.InternId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}