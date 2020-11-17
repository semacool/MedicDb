namespace Medic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBarcodeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BarcodeChars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(),
                        Pattern = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BarcodeChars");
        }
    }
}
