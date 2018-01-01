namespace yevgeller2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActionTypeInTempStorageTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TempStorageTags", "Action", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TempStorageTags", "Action");
        }
    }
}
