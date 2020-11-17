namespace Medic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalizeBlood", "Blood", c => c.Single(nullable: false));
            AddColumn("dbo.AnalizeMocha", "Mocha", c => c.Single(nullable: false));
            AddColumn("dbo.AnalizeShit", "Shit", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnalizeShit", "Shit");
            DropColumn("dbo.AnalizeMocha", "Mocha");
            DropColumn("dbo.AnalizeBlood", "Blood");
        }
    }
}
