namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IEnurebleTopic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        String1Name = c.String(),
                        String1Visible = c.Boolean(nullable: false),
                        String2Name = c.String(),
                        String2Visible = c.Boolean(nullable: false),
                        String3Name = c.String(),
                        String3Visible = c.Boolean(nullable: false),
                        Number1Name = c.String(),
                        Number1Visible = c.Boolean(nullable: false),
                        Number2Name = c.String(),
                        Number2Visible = c.Boolean(nullable: false),
                        Number3Name = c.String(),
                        Number3Visible = c.Boolean(nullable: false),
                        Date1Name = c.String(),
                        Date1Visible = c.Boolean(nullable: false),
                        Date2Name = c.String(),
                        Date2Visible = c.Boolean(nullable: false),
                        Date3Name = c.String(),
                        Date3Visible = c.Boolean(nullable: false),
                        Checkbox1Name = c.String(),
                        CheckBox1Visible = c.Boolean(nullable: false),
                        Checkbox2Name = c.String(),
                        CheckBox2Visible = c.Boolean(nullable: false),
                        Checkbox3Name = c.String(),
                        CheckBox3Visible = c.Boolean(nullable: false),
                        MarkdownField1Name = c.String(),
                        MarkdownField1Visible = c.Boolean(nullable: false),
                        MarkdownField2Name = c.String(),
                        MarkdownField2Visible = c.Boolean(nullable: false),
                        MarkdownField3Name = c.String(),
                        MarkdownField3Visible = c.Boolean(nullable: false),
                        CollectionTopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CollectionTopics", t => t.CollectionTopicId, cascadeDelete: true)
                .Index(t => t.CollectionTopicId);
            
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
            
            AddColumn("dbo.AspNetUsers", "IsBlocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCollections", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCollections", "CollectionId", "dbo.Collections");
            DropForeignKey("dbo.CollectionTopics", "Collection_Id", "dbo.Collections");
            DropForeignKey("dbo.Collections", "CollectionTopicId", "dbo.CollectionTopics");
            DropIndex("dbo.UserCollections", new[] { "CollectionId" });
            DropIndex("dbo.UserCollections", new[] { "UserId" });
            DropIndex("dbo.CollectionTopics", new[] { "Collection_Id" });
            DropIndex("dbo.Collections", new[] { "CollectionTopicId" });
            DropColumn("dbo.AspNetUsers", "IsBlocked");
            DropTable("dbo.UserCollections");
            DropTable("dbo.CollectionTopics");
            DropTable("dbo.Collections");
        }
    }
}
