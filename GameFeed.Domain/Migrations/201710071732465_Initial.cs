namespace GameFeed.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameCompanies",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.CompanyId })
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Summary = c.String(),
                        FirstReleaseDate = c.DateTime(nullable: false),
                        Rating = c.Single(nullable: false),
                        Cover_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Cover_ID)
                .Index(t => t.Cover_ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.GamePlatforms",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        PlatformId = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.PlatformId })
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Platforms", t => t.PlatformId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreGames",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Game_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameCompanies", "GameId", "dbo.Games");
            DropForeignKey("dbo.Images", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GenreGames", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.GamePlatforms", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.GamePlatforms", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "Cover_ID", "dbo.Images");
            DropForeignKey("dbo.GameCompanies", "CompanyId", "dbo.Companies");
            DropIndex("dbo.GenreGames", new[] { "Game_Id" });
            DropIndex("dbo.GenreGames", new[] { "Genre_Id" });
            DropIndex("dbo.GamePlatforms", new[] { "PlatformId" });
            DropIndex("dbo.GamePlatforms", new[] { "GameId" });
            DropIndex("dbo.Images", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Cover_ID" });
            DropIndex("dbo.GameCompanies", new[] { "CompanyId" });
            DropIndex("dbo.GameCompanies", new[] { "GameId" });
            DropTable("dbo.GenreGames");
            DropTable("dbo.Genres");
            DropTable("dbo.Platforms");
            DropTable("dbo.GamePlatforms");
            DropTable("dbo.Images");
            DropTable("dbo.Games");
            DropTable("dbo.GameCompanies");
            DropTable("dbo.Companies");
        }
    }
}
