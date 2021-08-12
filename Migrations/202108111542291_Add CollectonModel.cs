namespace kursach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollectonModel : DbMigration
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
                        Topic = c.String(),
                        String1 = c.String(),
                        String2 = c.String(),
                        String3 = c.String(),
                        Number1 = c.Int(nullable: false),
                        Number2 = c.Int(nullable: false),
                        Number3 = c.Int(nullable: false),
                        Date1 = c.DateTime(nullable: false),
                        Date2 = c.DateTime(nullable: false),
                        Date3 = c.DateTime(nullable: false),
                        Checkbox1 = c.Boolean(nullable: false),
                        Checkbox2 = c.Boolean(nullable: false),
                        Checkbox3 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Collections");
        }
    }
}
