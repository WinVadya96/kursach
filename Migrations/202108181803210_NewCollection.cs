namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCollection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Collections", "MarkdownField1Name", c => c.String());
            AddColumn("dbo.Collections", "MarkdownField1Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "MarkdownField2Name", c => c.String());
            AddColumn("dbo.Collections", "MarkdownField2Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "MarkdownField3Name", c => c.String());
            AddColumn("dbo.Collections", "MarkdownField3Visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Collections", "MarkdownField3Visible");
            DropColumn("dbo.Collections", "MarkdownField3Name");
            DropColumn("dbo.Collections", "MarkdownField2Visible");
            DropColumn("dbo.Collections", "MarkdownField2Name");
            DropColumn("dbo.Collections", "MarkdownField1Visible");
            DropColumn("dbo.Collections", "MarkdownField1Name");
        }
    }
}
