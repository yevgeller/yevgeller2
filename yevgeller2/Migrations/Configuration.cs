namespace yevgeller2.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using yevgeller2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<yevgeller2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            context.Projects.AddOrUpdate(p => p.Id,
                new Project
                {
                    Name = "yevgeller.net",
                    Description = "I wrote this web site. I use it as a sandbox for trying out new stuff.",
                    Year = "2013-present",
                    Technology = "ASP.Net MVC",
                    Url = "http://yevgeller.net",
                    Tags = new List<Tag>
                        {
                            new Tag {  Name="C#"},
                            new Tag {  Name=".Net"},
                            new Tag {  Name="ASP.Net"},
                            new Tag {  Name="JavaScript"},
                            new Tag {  Name = "SQL" },
                            new Tag {  Name = "Database" },
                            new Tag {  Name = "jQuery" }
                        }
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
