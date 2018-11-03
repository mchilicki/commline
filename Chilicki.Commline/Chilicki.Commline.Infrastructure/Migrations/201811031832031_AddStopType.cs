namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddStopType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stops", "StopType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stops", "StopType");
        }
    }
}
