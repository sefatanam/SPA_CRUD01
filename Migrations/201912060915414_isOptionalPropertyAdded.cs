namespace SPA_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isOptionalPropertyAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "IsOptional", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "IsOptional");
        }
    }
}
