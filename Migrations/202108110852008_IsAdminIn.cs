namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAdminIn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsAdminIn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsAdminIn");
        }
    }
}
