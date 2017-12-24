using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace yevgeller2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<MyProject> MyProjects { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyProjectTag>()
                .HasKey(k => new { k.MyProjecTId, k.TagId });

            modelBuilder.Entity<MyProjectTag>()
                .HasRequired(m => m.MyProject)
                .WithMany(t => t.Tags)
                .HasForeignKey(t => t.TagId);

            modelBuilder.Entity<MyProjectTag>()
                .HasRequired(m => m.Tag)
                .WithMany(t => t.MyProjects)
                .HasForeignKey(mt => mt.MyProjecTId);

            //modelBuilder.Entity<Tag>()
            //    .HasOne

            //modelBuilder.Entity<MyProject>()
            //    .HasMany(p => p.Tags)
            //    .WithMany();
                

            base.OnModelCreating(modelBuilder); 
        }
    }
}