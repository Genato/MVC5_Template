using Microsoft.AspNet.Identity.EntityFramework;
using MVC5_Template.Models;
using MVC5_Template.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC5_Template.DbContexts
{
    public class IdentityUserDbContext : IdentityDbContext<MVC5_TemplateIdentityUser>
    {
        public IdentityUserDbContext()
            : base("PostgreMVC5_TemplateIdentityConnection", throwIfV1Schema: false)
        {
        }

        public static IdentityUserDbContext Create()
        {
            return new IdentityUserDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("identity");

            base.OnModelCreating(modelBuilder);

            //Users
            modelBuilder.Entity<MVC5_TemplateIdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            //Roles
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
        }
    }
}