namespace RunTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Shoes", "MilesRun");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shoes", "MilesRun", c => c.Double());
        }
    }
}
