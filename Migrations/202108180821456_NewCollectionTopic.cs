namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCollectionTopic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollectionTopics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Collection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collections", t => t.Collection_Id)
                .Index(t => t.Collection_Id);
            
            CreateTable(
                "dbo.UserCollections",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CollectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CollectionId })
                .ForeignKey("dbo.Collections", t => t.CollectionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CollectionId);
            
            AddColumn("dbo.Collections", "String1Name", c => c.String());
            AddColumn("dbo.Collections", "String1Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "String2Name", c => c.String());
            AddColumn("dbo.Collections", "String2Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "String3Name", c => c.String());
            AddColumn("dbo.Collections", "String3Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Number1Name", c => c.String());
            AddColumn("dbo.Collections", "Number1Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Number2Name", c => c.String());
            AddColumn("dbo.Collections", "Number2Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Number3Name", c => c.String());
            AddColumn("dbo.Collections", "Number3Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Date1Name", c => c.String());
            AddColumn("dbo.Collections", "Date1Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Date2Name", c => c.String());
            AddColumn("dbo.Collections", "Date2Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Date3Name", c => c.String());
            AddColumn("dbo.Collections", "Date3Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Checkbox1Name", c => c.String());
            AddColumn("dbo.Collections", "CheckBox1Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Checkbox2Name", c => c.String());
            AddColumn("dbo.Collections", "CheckBox2Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "Checkbox3Name", c => c.String());
            AddColumn("dbo.Collections", "CheckBox3Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Collections", "CollectonTopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.Collections", "CollectonTopicId");
            AddForeignKey("dbo.Collections", "CollectonTopicId", "dbo.CollectionTopics", "Id", cascadeDelete: true);
            DropTable("dbo.Topics");
        }
        
        public override void Down()
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
            
            DropForeignKey("dbo.UserCollections", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCollections", "CollectionId", "dbo.Collections");
            DropForeignKey("dbo.CollectionTopics", "Collection_Id", "dbo.Collections");
            DropForeignKey("dbo.Collections", "CollectonTopicId", "dbo.CollectionTopics");
            DropIndex("dbo.UserCollections", new[] { "CollectionId" });
            DropIndex("dbo.UserCollections", new[] { "UserId" });
            DropIndex("dbo.CollectionTopics", new[] { "Collection_Id" });
            DropIndex("dbo.Collections", new[] { "CollectonTopicId" });
            DropColumn("dbo.Collections", "CollectonTopicId");
            DropColumn("dbo.Collections", "CheckBox3Visible");
            DropColumn("dbo.Collections", "Checkbox3Name");
            DropColumn("dbo.Collections", "CheckBox2Visible");
            DropColumn("dbo.Collections", "Checkbox2Name");
            DropColumn("dbo.Collections", "CheckBox1Visible");
            DropColumn("dbo.Collections", "Checkbox1Name");
            DropColumn("dbo.Collections", "Date3Visible");
            DropColumn("dbo.Collections", "Date3Name");
            DropColumn("dbo.Collections", "Date2Visible");
            DropColumn("dbo.Collections", "Date2Name");
            DropColumn("dbo.Collections", "Date1Visible");
            DropColumn("dbo.Collections", "Date1Name");
            DropColumn("dbo.Collections", "Number3Visible");
            DropColumn("dbo.Collections", "Number3Name");
            DropColumn("dbo.Collections", "Number2Visible");
            DropColumn("dbo.Collections", "Number2Name");
            DropColumn("dbo.Collections", "Number1Visible");
            DropColumn("dbo.Collections", "Number1Name");
            DropColumn("dbo.Collections", "String3Visible");
            DropColumn("dbo.Collections", "String3Name");
            DropColumn("dbo.Collections", "String2Visible");
            DropColumn("dbo.Collections", "String2Name");
            DropColumn("dbo.Collections", "String1Visible");
            DropColumn("dbo.Collections", "String1Name");
            DropTable("dbo.UserCollections");
            DropTable("dbo.CollectionTopics");
        }
    }
}
