namespace ParseLogFile.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedateTimetostringinDataLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "NominationNetwork", c => c.String());
            AddColumn("dbo.DataLogs", "DateRequest", c => c.String());
            AddColumn("dbo.DataLogs", "TimeRequest", c => c.String());
            DropColumn("dbo.DataLogs", "DataRequest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataLogs", "DataRequest", c => c.DateTime(nullable: false));
            DropColumn("dbo.DataLogs", "TimeRequest");
            DropColumn("dbo.DataLogs", "DateRequest");
            DropColumn("dbo.Companies", "NominationNetwork");
        }
    }
}
