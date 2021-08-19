namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCollectionTopicName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Collections", "TopicName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Collections", "TopicName", c => c.String());
        }
    }
}
