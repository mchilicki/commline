namespace Chilicki.Commline.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLineColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lines", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "Color");
        }
    }
}
