namespace RunTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoeDescShoeNullableInWorkout : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workouts", "ShoeID", "dbo.Shoes");
            DropIndex("dbo.Workouts", new[] { "ShoeID" });
            AddColumn("dbo.Shoes", "Description", c => c.String());
            AlterColumn("dbo.Workouts", "ShoeID", c => c.Int());
            CreateIndex("dbo.Workouts", "ShoeID");
            AddForeignKey("dbo.Workouts", "ShoeID", "dbo.Shoes", "ShoeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "ShoeID", "dbo.Shoes");
            DropIndex("dbo.Workouts", new[] { "ShoeID" });
            AlterColumn("dbo.Workouts", "ShoeID", c => c.Int(nullable: false));
            DropColumn("dbo.Shoes", "Description");
            CreateIndex("dbo.Workouts", "ShoeID");
            AddForeignKey("dbo.Workouts", "ShoeID", "dbo.Shoes", "ShoeID", cascadeDelete: true);
        }
    }
}
