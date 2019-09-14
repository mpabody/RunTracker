namespace RunTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shoes", "MilesRun", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shoes", "MilesRun");
        }
    }
}
