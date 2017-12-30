namespace yevgeller2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebAPI_for_Project_Tags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempStorageTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdNo = c.Int(nullable: false),
                        Name = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TempStorageTags");
        }
    }
}
