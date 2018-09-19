namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateStopsLinesDeparturesAndRouteStations : DbMigration
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
                        RouteStation_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RouteStations", t => t.RouteStation_Id)
                .Index(t => t.RouteStation_Id);
            
            CreateTable(
                "dbo.RouteStations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RouteOrderId = c.Int(nullable: false),
                        Line_Id = c.Long(nullable: false),
                        Stop_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lines", t => t.Line_Id)
                .ForeignKey("dbo.Stops", t => t.Stop_Id)
                .Index(t => t.Line_Id)
                .Index(t => t.Stop_Id);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Number = c.String(nullable: false),
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
                        SiteNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteStations", "Stop_Id", "dbo.Stops");
            DropForeignKey("dbo.RouteStations", "Line_Id", "dbo.Lines");
            DropForeignKey("dbo.Departures", "RouteStation_Id", "dbo.RouteStations");
            DropIndex("dbo.RouteStations", new[] { "Stop_Id" });
            DropIndex("dbo.RouteStations", new[] { "Line_Id" });
            DropIndex("dbo.Departures", new[] { "RouteStation_Id" });
            DropTable("dbo.Stops");
            DropTable("dbo.Lines");
            DropTable("dbo.RouteStations");
            DropTable("dbo.Departures");
        }
    }
}
