namespace GameFeed.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratingadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Rating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Rating");
        }
    }
}
