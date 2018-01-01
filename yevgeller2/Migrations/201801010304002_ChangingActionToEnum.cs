namespace yevgeller2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingActionToEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TempStorageTags", "Action", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TempStorageTags", "Action", c => c.String());
        }
    }
}
