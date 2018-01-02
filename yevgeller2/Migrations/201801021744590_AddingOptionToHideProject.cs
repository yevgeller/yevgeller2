namespace yevgeller2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingOptionToHideProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "IsHidden", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "IsHidden");
        }
    }
}
