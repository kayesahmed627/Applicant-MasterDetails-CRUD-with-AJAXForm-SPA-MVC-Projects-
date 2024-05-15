namespace MVC_PROJECT_1278941.Migrations.ContextA
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "PayRate", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "PayRate");
        }
    }
}
