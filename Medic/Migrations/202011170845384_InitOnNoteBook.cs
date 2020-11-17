namespace Medic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitOnNoteBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fio = c.String(nullable: false, maxLength: 255),
                        Salary = c.Decimal(nullable: false, storeType: "money"),
                        Birthday = c.DateTime(nullable: false, storeType: "date"),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        IdType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.IdType)
                .Index(t => t.IdType);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Middlename = c.String(nullable: false, maxLength: 50),
                        Birthday = c.DateTime(nullable: false, storeType: "date"),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Analizi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdClient = c.Int(nullable: false),
                        IdAnalizType = c.Int(nullable: false),
                        IdLaborant = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnalizTypes", t => t.IdAnalizType)
                .ForeignKey("dbo.Laborants", t => t.IdLaborant)
                .ForeignKey("dbo.Clients", t => t.IdClient)
                .Index(t => t.IdClient)
                .Index(t => t.IdAnalizType)
                .Index(t => t.IdLaborant);
            
            CreateTable(
                "dbo.AnalizeBlood",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Zhelezo = c.Single(nullable: false),
                        Blood = c.Single(nullable: false),
                        IdAnalize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Analizi", t => t.IdAnalize, cascadeDelete: true)
                .Index(t => t.IdAnalize);
            
            CreateTable(
                "dbo.AnalizeMocha",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZhelezoMocha = c.Single(nullable: false),
                        Mocha = c.Single(nullable: false),
                        IdAnalize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Analizi", t => t.IdAnalize, cascadeDelete: true)
                .Index(t => t.IdAnalize);
            
            CreateTable(
                "dbo.AnalizeShit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZhelezoShit = c.Single(nullable: false),
                        Shit = c.Single(nullable: false),
                        IdAnalize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Analizi", t => t.IdAnalize, cascadeDelete: true)
                .Index(t => t.IdAnalize);
            
            CreateTable(
                "dbo.AnalizTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Laborants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fio = c.String(nullable: false, maxLength: 255),
                        Salary = c.Decimal(nullable: false, storeType: "money"),
                        Birthday = c.DateTime(nullable: false, storeType: "date"),
                        IdUser = c.Int(nullable: false),
                        IdLaboratory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Laboratories", t => t.IdLaboratory, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdLaboratory);
            
            CreateTable(
                "dbo.Laboratories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 255),
                        Title = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BarcodeChars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Character = c.String(maxLength: 8000, fixedLength: true, unicode: false),
                        Value = c.Int(),
                        Pattern = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "IdType", "dbo.UserTypes");
            DropForeignKey("dbo.Laborants", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Clients", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Analizi", "IdClient", "dbo.Clients");
            DropForeignKey("dbo.Laborants", "IdLaboratory", "dbo.Laboratories");
            DropForeignKey("dbo.Analizi", "IdLaborant", "dbo.Laborants");
            DropForeignKey("dbo.Analizi", "IdAnalizType", "dbo.AnalizTypes");
            DropForeignKey("dbo.AnalizeShit", "IdAnalize", "dbo.Analizi");
            DropForeignKey("dbo.AnalizeMocha", "IdAnalize", "dbo.Analizi");
            DropForeignKey("dbo.AnalizeBlood", "IdAnalize", "dbo.Analizi");
            DropForeignKey("dbo.Admins", "IdUser", "dbo.Users");
            DropIndex("dbo.Laborants", new[] { "IdLaboratory" });
            DropIndex("dbo.Laborants", new[] { "IdUser" });
            DropIndex("dbo.AnalizeShit", new[] { "IdAnalize" });
            DropIndex("dbo.AnalizeMocha", new[] { "IdAnalize" });
            DropIndex("dbo.AnalizeBlood", new[] { "IdAnalize" });
            DropIndex("dbo.Analizi", new[] { "IdLaborant" });
            DropIndex("dbo.Analizi", new[] { "IdAnalizType" });
            DropIndex("dbo.Analizi", new[] { "IdClient" });
            DropIndex("dbo.Clients", new[] { "IdUser" });
            DropIndex("dbo.Users", new[] { "IdType" });
            DropIndex("dbo.Admins", new[] { "IdUser" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.BarcodeChars");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Laboratories");
            DropTable("dbo.Laborants");
            DropTable("dbo.AnalizTypes");
            DropTable("dbo.AnalizeShit");
            DropTable("dbo.AnalizeMocha");
            DropTable("dbo.AnalizeBlood");
            DropTable("dbo.Analizi");
            DropTable("dbo.Clients");
            DropTable("dbo.Users");
            DropTable("dbo.Admins");
        }
    }
}
