namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddRunIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departures", "RunIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departures", "RunIndex");
        }
    }
}
