namespace GameFeed.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameCompanyRoleasPK : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GameCompanies");
            AddPrimaryKey("dbo.GameCompanies", new[] { "GameId", "CompanyId", "Role" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.GameCompanies");
            AddPrimaryKey("dbo.GameCompanies", new[] { "GameId", "CompanyId" });
        }
    }
}
