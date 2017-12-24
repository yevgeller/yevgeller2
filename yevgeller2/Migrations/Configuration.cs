using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
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
            context.MyProjects.AddOrUpdate(p => p.Id,
                new MyProject
                {
                    Name = "yevgeller.net",
                    Description = "I wrote this web site. I use it as a sandbox for trying out new stuff.",
                    Year = "2013-present",
                    Technology = "ASP.Net MVC",
                    Url = "http://yevgeller.net",
                //    Tags = new List<Tag>
                //    {
                //        new Tag {  Name="C#"},
                //new Tag {  Name=".Net"},
                //new Tag {  Name="WPF"},
                //new Tag {  Name="Universal Windows Platform"},
                //new Tag {  Name="ASP.Net"},
                //new Tag {  Name="JavaScript"},
                //new Tag {  Name = "SQL" },
                //new Tag {  Name = "Database" },
                //new Tag {  Name = "jQuery" }
                //    }
                });

            context.Tags.AddOrUpdate(t => t.Name,
                new Tag { Name = "C#" },
                new Tag { Name = ".Net" },
                new Tag { Name = "WPF" },
                new Tag { Name = "Universal Windows Platform" },
                new Tag { Name = "ASP.Net" },
                new Tag { Name = "JavaScript" },
                new Tag { Name = "SQL" },
                new Tag { Name = "Database" },
                new Tag { Name = "jQuery" }
                );

            //_db.Tags.AddOrUpdate(t => t.Name,
            //    new Tag { Id = 1, Name = "C#" },
            //    new Tag { Id = 2, Name = ".Net" },
            //    new Tag { Id = 3, Name = "WPF" },
            //    new Tag { Id = 4, Name = "Universal Windows Platform" },
            //    new Tag { Id = 5, Name = "ASP.Net" },
            //    new Tag { Id = 6, Name = "JavaScript" },
            //    new Tag { Id = 7, Name = "SQL" },
            //    new Tag { Id = 8, Name = "Database" },
            //    new Tag { Id = 9, Name = "jQuery" });
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
