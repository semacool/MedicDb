namespace Medic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBarcodeTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BarcodeChars", "Character", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BarcodeChars", "Character");
        }
    }
}
