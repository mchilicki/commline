namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DepartureTime = c.Time(nullable: false, precision: 7),
                        DayType = c.Int(nullable: false),
                        RouteStop_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RouteStops", t => t.RouteStop_Id, cascadeDelete: true)
                .Index(t => t.RouteStop_Id);
            
            CreateTable(
                "dbo.RouteStops",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StopIndex = c.Int(nullable: false),
                        Line_Id = c.Long(nullable: false),
                        Stop_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lines", t => t.Line_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stops", t => t.Stop_Id, cascadeDelete: true)
                .Index(t => t.Line_Id)
                .Index(t => t.Stop_Id);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LineType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stops",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        StopNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departures", "RouteStop_Id", "dbo.RouteStops");
            DropForeignKey("dbo.RouteStops", "Stop_Id", "dbo.Stops");
            DropForeignKey("dbo.RouteStops", "Line_Id", "dbo.Lines");
            DropIndex("dbo.RouteStops", new[] { "Stop_Id" });
            DropIndex("dbo.RouteStops", new[] { "Line_Id" });
            DropIndex("dbo.Departures", new[] { "RouteStop_Id" });
            DropTable("dbo.Stops");
            DropTable("dbo.Lines");
            DropTable("dbo.RouteStops");
            DropTable("dbo.Departures");
        }
    }
}
