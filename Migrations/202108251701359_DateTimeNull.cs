namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CollectionItems", "Date1Value", c => c.DateTime());
            AlterColumn("dbo.CollectionItems", "Date2Value", c => c.DateTime());
            AlterColumn("dbo.CollectionItems", "Date3Value", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CollectionItems", "Date3Value", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CollectionItems", "Date2Value", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CollectionItems", "Date1Value", c => c.DateTime(nullable: false));
        }
    }
}
