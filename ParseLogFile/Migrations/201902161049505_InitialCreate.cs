namespace ParseLogFile.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataLogs", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.DataLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataRequest = c.DateTime(nullable: false),
                        TypeRequest = c.String(),
                        RezultRequest = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Path = c.String(),
                        Size = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataLogs", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "Id", "dbo.DataLogs");
            DropForeignKey("dbo.Files", "Id", "dbo.DataLogs");
            DropIndex("dbo.Files", new[] { "Id" });
            DropIndex("dbo.Companies", new[] { "Id" });
            DropTable("dbo.Files");
            DropTable("dbo.DataLogs");
            DropTable("dbo.Companies");
        }
    }
}
