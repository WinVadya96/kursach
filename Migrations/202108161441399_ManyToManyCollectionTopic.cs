namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyCollectionTopic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollectionTopics",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TopicId = c.String(nullable: false, maxLength: 128),
                        Topic_TopicId = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.TopicId })
                .ForeignKey("dbo.Collections", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.Topic_TopicId)
                .Index(t => t.Id)
                .Index(t => t.Topic_TopicId);
            
            CreateTable(
                "dbo.UserCollections",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NameId = c.String(nullable: false, maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.NameId })
                .ForeignKey("dbo.Collections", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Topics", "BoolForString1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForString2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForString3", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForNumber1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForNumber2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForNumber3", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForDateTime1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForDateTime2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForDateTime3", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForCheckBox1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForCheckBox2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Topics", "BoolForCheckBox3", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCollections", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCollections", "Id", "dbo.Collections");
            DropForeignKey("dbo.CollectionTopics", "Topic_TopicId", "dbo.Topics");
            DropForeignKey("dbo.CollectionTopics", "Id", "dbo.Collections");
            DropIndex("dbo.UserCollections", new[] { "User_Id" });
            DropIndex("dbo.UserCollections", new[] { "Id" });
            DropIndex("dbo.CollectionTopics", new[] { "Topic_TopicId" });
            DropIndex("dbo.CollectionTopics", new[] { "Id" });
            DropColumn("dbo.Topics", "BoolForCheckBox3");
            DropColumn("dbo.Topics", "BoolForCheckBox2");
            DropColumn("dbo.Topics", "BoolForCheckBox1");
            DropColumn("dbo.Topics", "BoolForDateTime3");
            DropColumn("dbo.Topics", "BoolForDateTime2");
            DropColumn("dbo.Topics", "BoolForDateTime1");
            DropColumn("dbo.Topics", "BoolForNumber3");
            DropColumn("dbo.Topics", "BoolForNumber2");
            DropColumn("dbo.Topics", "BoolForNumber1");
            DropColumn("dbo.Topics", "BoolForString3");
            DropColumn("dbo.Topics", "BoolForString2");
            DropColumn("dbo.Topics", "BoolForString1");
            DropTable("dbo.UserCollections");
            DropTable("dbo.CollectionTopics");
        }
    }
}
