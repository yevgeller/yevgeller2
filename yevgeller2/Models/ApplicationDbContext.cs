using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace yevgeller2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Project> Projects { get; set; }
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
            modelBuilder.Entity<Project>()
                .HasMany<Tag>(p => p.Tags)
                .WithMany(t => t.Projects)
                .Map(pt =>
                {
                    pt.MapLeftKey("ProjectId");
                    pt.MapLeftKey("TagId");
                    pt.ToTable("ProjectTag");
                });

            //modelBuilder.Entity<ProjectTag>()
            //    .HasKey(k => new { k.ProjectId, k.TagId });

            //modelBuilder.Entity<ProjectTag>()
            //    .HasRequired(m => m.Project)
            //    .WithMany(t => t.Tags)
            //    .HasForeignKey(t => t.TagId);

            //modelBuilder.Entity<ProjectTag>()
            //    .HasRequired(m => m.Tag)
            //    .WithMany(t => t.Projects)
            //    .HasForeignKey(mt => mt.ProjectId);

            //modelBuilder.Entity<Tag>()
            //    .HasOne

            //modelBuilder.Entity<MyProject>()
            //    .HasMany(p => p.Tags)
            //    .WithMany();
                

            base.OnModelCreating(modelBuilder); 
        }
    }
}