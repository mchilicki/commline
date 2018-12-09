namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddIsOnNextDayInDeparture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departures", "IsOnNextDay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departures", "IsOnNextDay");
        }
    }
}
