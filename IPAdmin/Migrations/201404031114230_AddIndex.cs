namespace IPAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SerialNoes", "SerialNumber", true, "IX_INDEX");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SerialNoes", "SerialNumber");
        }
    }
}
