using MVC5_Template.Models.MVC5_TemplateModels;
using System.Data.Entity;

namespace MVC5_Template.DbContexts
{
  public class MVC5_TemplateDbContext : DbContext
    {
        public MVC5_TemplateDbContext()
            : base("PostgreMVC5_TemplateConnection")
        {
        }

        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Locale> Localization { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("mvc5template");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserSettings>().ToTable("User_Settings");
            modelBuilder.Entity<Locale>().ToTable("Localization").Property(p => p._Localization).HasColumnName("Localization");
        }
    }
}