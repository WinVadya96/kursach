namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAdminInOf : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IsAdminIn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsAdminIn", c => c.Boolean(nullable: false));
        }
    }
}
