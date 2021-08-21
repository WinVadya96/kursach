namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CollectionItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CollectionTopics", "Collection_Id", "dbo.Collections");
            DropIndex("dbo.CollectionTopics", new[] { "Collection_Id" });
            CreateTable(
                "dbo.CollectionItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CollectionId = c.Int(nullable: false),
                        String1Value = c.String(),
                        String2Value = c.String(),
                        String3Value = c.String(),
                        Number1Value = c.Double(nullable: false),
                        Number2Value = c.Double(nullable: false),
                        Number3Value = c.Double(nullable: false),
                        Date1Value = c.DateTime(nullable: false),
                        Date2Value = c.DateTime(nullable: false),
                        Date3Value = c.DateTime(nullable: false),
                        Markdown1Value = c.String(),
                        Markdown2Value = c.String(),
                        Markdown3Value = c.String(),
                        Checkbox1Value = c.Boolean(nullable: false),
                        Checkbox2Value = c.Boolean(nullable: false),
                        Checkbox3Value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collections", t => t.CollectionId, cascadeDelete: true)
                .Index(t => t.CollectionId);
            
            DropColumn("dbo.CollectionTopics", "Collection_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectionTopics", "Collection_Id", c => c.Int());
            DropForeignKey("dbo.CollectionItems", "CollectionId", "dbo.Collections");
            DropIndex("dbo.CollectionItems", new[] { "CollectionId" });
            DropTable("dbo.CollectionItems");
            CreateIndex("dbo.CollectionTopics", "Collection_Id");
            AddForeignKey("dbo.CollectionTopics", "Collection_Id", "dbo.Collections", "Id");
        }
    }
}
