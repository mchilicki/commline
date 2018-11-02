namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReturningAndCircularLines : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteStops", "IsReturnStop", c => c.Boolean(nullable: false));
            AddColumn("dbo.Lines", "IsCircular", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "IsCircular");
            DropColumn("dbo.RouteStops", "IsReturnStop");
        }
    }
}
