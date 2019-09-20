namespace RunTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RaceTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Workouts");
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        RaceID = c.Int(nullable: false, identity: true),
                        ShoeID = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Distance = c.Double(nullable: false),
                        Description = c.String(),
                        Comments = c.String(),
                        CompletionTime = c.String(),
                    })
                .PrimaryKey(t => t.RaceID)
                .ForeignKey("dbo.Shoes", t => t.ShoeID)
                .Index(t => t.ShoeID);
            
            AddColumn("dbo.Workouts", "WorkoutID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Workouts", "WorkoutID");
            DropColumn("dbo.Workouts", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workouts", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Races", "ShoeID", "dbo.Shoes");
            DropIndex("dbo.Races", new[] { "ShoeID" });
            DropPrimaryKey("dbo.Workouts");
            DropColumn("dbo.Workouts", "WorkoutID");
            DropTable("dbo.Races");
            AddPrimaryKey("dbo.Workouts", "ID");
        }
    }
}
