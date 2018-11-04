namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveIsReturnStop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RouteStops", "IsReturnStop");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RouteStops", "IsReturnStop", c => c.Boolean(nullable: false));
        }
    }
}
