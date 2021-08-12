namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTopicTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        String1 = c.String(),
                        String2 = c.String(),
                        String3 = c.String(),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Date1 = c.DateTime(),
                        Date2 = c.DateTime(),
                        Date3 = c.DateTime(),
                        Checkbox1 = c.Boolean(nullable: false),
                        Checkbox2 = c.Boolean(nullable: false),
                        Checkbox3 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TopicId);
            
            AddColumn("dbo.Collections", "TopicName", c => c.String());
            DropColumn("dbo.Collections", "Topic");
            DropColumn("dbo.Collections", "String1");
            DropColumn("dbo.Collections", "String2");
            DropColumn("dbo.Collections", "String3");
            DropColumn("dbo.Collections", "Number1");
            DropColumn("dbo.Collections", "Number2");
            DropColumn("dbo.Collections", "Number3");
            DropColumn("dbo.Collections", "Date1");
            DropColumn("dbo.Collections", "Date2");
            DropColumn("dbo.Collections", "Date3");
            DropColumn("dbo.Collections", "Checkbox1");
            DropColumn("dbo.Collections", "Checkbox2");
            DropColumn("dbo.Collections", "Checkbox3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Collections", "Checkbox3", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Checkbox2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Checkbox1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Date3", c => c.DateTime(nullable: false));
            AddColumn("dbo.Collections", "Date2", c => c.DateTime(nullable: false));
            AddColumn("dbo.Collections", "Date1", c => c.DateTime(nullable: false));
            AddColumn("dbo.Collections", "Number3", c => c.Int(nullable: false));
            AddColumn("dbo.Collections", "Number2", c => c.Int(nullable: false));
            AddColumn("dbo.Collections", "Number1", c => c.Int(nullable: false));
            AddColumn("dbo.Collections", "String3", c => c.String());
            AddColumn("dbo.Collections", "String2", c => c.String());
            AddColumn("dbo.Collections", "String1", c => c.String());
            AddColumn("dbo.Collections", "Topic", c => c.String());
            DropColumn("dbo.Collections", "TopicName");
            DropTable("dbo.Topics");
        }
    }
}
