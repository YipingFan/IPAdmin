namespace IPAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestrictLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SerialNoes", "SerialNumber", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SerialNoes", "SerialNumber", c => c.String(nullable: false));
        }
    }
}
