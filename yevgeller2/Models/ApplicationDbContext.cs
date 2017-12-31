using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace yevgeller2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TempStorageTag> TempStorageTags { get; set; }

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
                    pt.MapRightKey("TagId");
                    pt.ToTable("ProjectTag");
                });

            base.OnModelCreating(modelBuilder);
        }

        internal IEnumerable<Tag> GetExistingSelectedTags(string userId, int sessionNo)
        {
            List<TempStorageTag> tempSelectedTags = TempStorageTags
                .Where(x => x.UserId == userId && x.IdNo == sessionNo).ToList();

            List<Tag> allTags = Tags.ToList();

            List<Tag> result = new List<Tag>();

            foreach (TempStorageTag tst in tempSelectedTags)
            {
                Tag t = allTags.Where(x => x.Name == tst.Name).FirstOrDefault();
                if (t != null) result.Add(t);
            }

            tempSelectedTags.ForEach(x => this.TempStorageTags.Remove(x));

            return result;
        }
    }
}