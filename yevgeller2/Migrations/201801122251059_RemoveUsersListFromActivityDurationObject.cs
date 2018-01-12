namespace yevgeller2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUsersListFromActivityDurationObject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ActivityDurationRecord_Id", "dbo.ActivityDurationRecords");
            DropIndex("dbo.AspNetUsers", new[] { "ActivityDurationRecord_Id" });
            DropColumn("dbo.AspNetUsers", "ActivityDurationRecord_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ActivityDurationRecord_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ActivityDurationRecord_Id");
            AddForeignKey("dbo.AspNetUsers", "ActivityDurationRecord_Id", "dbo.ActivityDurationRecords", "Id");
        }
    }
}
