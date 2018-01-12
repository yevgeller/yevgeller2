namespace yevgeller2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedtableforFunTimer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityDurationRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        DurationInSeconds = c.Int(nullable: false),
                        RecordType = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActivityDurationRecords");
        }
    }
}
