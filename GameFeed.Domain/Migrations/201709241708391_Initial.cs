namespace GameFeed.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Summary = c.String(),
                        FirstReleaseDate = c.DateTime(nullable: false),
                        Cover_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.Cover_ID)
                .Index(t => t.Cover_ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Cover_ID", "dbo.Images");
            DropIndex("dbo.Games", new[] { "Cover_ID" });
            DropTable("dbo.Images");
            DropTable("dbo.Games");
        }
    }
}
