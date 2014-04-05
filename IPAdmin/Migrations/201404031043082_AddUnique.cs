namespace IPAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SerialNoes", "SerialNumber", c => c.String(nullable: false));
            //CreateIndex("dbo.SerialNoes", "SerialNumber", true, "IX_INDEX");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SerialNoes", "SerialNumber", c => c.String());
            //DropIndex("dbo.SerialNoes", "SerialNumber");
        }
    }
}
