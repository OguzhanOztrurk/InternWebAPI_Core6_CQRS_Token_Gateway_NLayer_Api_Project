

using DataAccess.Concrete.Configurations;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataAccess.Concrete.EntityFramework.Contexts;

public class AppDbContext:DbContext
{
    protected readonly IConfiguration _configuration;


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region DbSet
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Education> Educations { get; set; }
    public virtual DbSet<Intern> Interns { get; set; }
    public virtual DbSet<Talent> Talents { get; set; }
    public virtual DbSet<WorkHistory> WorkHistories { get; set; }
    public virtual DbSet<Workplace> Workplaces { get; set; }
    #endregion
        
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        #region Configurations
        new AdminConfiguration().Configure(modelBuilder.Entity<Admin>());
        new EducationConfiguration().Configure(modelBuilder.Entity<Education>());
        new InternConfiguration().Configure(modelBuilder.Entity<Intern>());
        new TalentConfiguration().Configure(modelBuilder.Entity<Talent>());
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new WorkHistoryConfiguration().Configure(modelBuilder.Entity<WorkHistory>());
        new WorkplaceConfiguration().Configure(modelBuilder.Entity<Workplace>());
        
        #endregion

        #region Seeds
        //new TypeSeed().Configure(modelBuilder.Entity<Type>());
        
        #endregion
        
        //Migration Ekleme //==> dotnet ef migrations add Initial -s IdentityAPI -p DataAccess
        //Veritabanı Güncelleme //==> dotnet ef database update -s IdentityAPI -p DataAccess
           
    }
}