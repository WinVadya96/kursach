namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsBlocked", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "IsEnabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsEnabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "IsBlocked");
        }
    }
}
