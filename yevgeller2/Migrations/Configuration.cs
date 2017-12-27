
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using yevgeller2.Models;
namespace yevgeller2.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<yevgeller2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(yevgeller2.Models.ApplicationDbContext context)
        {
            Tag t1 = new Tag { Name = "C#" };
            Tag t2 = new Tag { Name = ".Net" };
            Tag t3 = new Tag { Name = "WPF" };
            Tag t4 = new Tag { Name = "Universal Windows Platform" };
            Tag t5 = new Tag { Name = "ASP.Net" };
            Tag t6 = new Tag { Name = "JavaScript" };
            Tag t7 = new Tag { Name = "SQL" };
            Tag t8 = new Tag { Name = "Database" };
            Tag t9 = new Tag { Name = "jQuery" };

            context.Tags.AddOrUpdate(t => t.Name, t1, t2, t3, t4, t5, t6, t7, t8, t9);


            List<Tag> allCandidateTags = new List<Tag>
            {
                t1, t2, t3, t4, t5, t6, t7,t8,t9
            };
            //context.Tags.AddOrUpdate(t => t.Name,  );

            var project1 = new Project
            {
                Name = "yevgeller.net",
                Description = "I wrote this web site. I use it as a sandbox for trying out new stuff.",
                Year = "2013-present",
                Technology = "ASP.Net MVC",
                Url = "http://yevgeller.net",
                Tags=new List<Tag>()
            };

            project1.Tags.Add(t1);
            project1.Tags.Add(t2);
            project1.Tags.Add(t5);
            project1.Tags.Add(t6);
            project1.Tags.Add(t7);
            project1.Tags.Add(t8);
            project1.Tags.Add(t9);

            context.Projects.AddOrUpdate(p=>p.Name, project1);
            context.SaveChanges();


            //context.Projects.AddOrUpdate(p => p.Id, project1);

            //Tags = new List<Tag>
            //    {
            //        new Tag {  Name="C#"},
            //        new Tag {  Name=".Net"},
            //        new Tag {  Name="ASP.Net"},
            //        new Tag {  Name="JavaScript"},
            //        new Tag {  Name = "SQL" },
            //        new Tag {  Name = "Database" },
            //        new Tag {  Name = "jQuery" }
            //    }
            //});



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}