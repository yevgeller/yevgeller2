namespace yevgeller2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMyProjectsAndTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false, maxLength: 1024),
                        Technology = c.String(nullable: false, maxLength: 128),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        MyProject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MyProjects", t => t.MyProject_Id)
                .Index(t => t.MyProject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "MyProject_Id", "dbo.MyProjects");
            DropIndex("dbo.Tags", new[] { "MyProject_Id" });
            DropTable("dbo.Tags");
            DropTable("dbo.MyProjects");
        }
    }
}
