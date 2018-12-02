namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsBetweenDaysInDeparture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departures", "IsBetweenDays", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departures", "IsBetweenDays");
        }
    }
}
