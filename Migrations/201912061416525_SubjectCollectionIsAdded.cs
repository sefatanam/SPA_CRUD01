namespace SPA_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubjectCollectionIsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "StudentSubject_Id", c => c.Int());
            CreateIndex("dbo.Subjects", "StudentSubject_Id");
            AddForeignKey("dbo.Subjects", "StudentSubject_Id", "dbo.StudentSubjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "StudentSubject_Id", "dbo.StudentSubjects");
            DropIndex("dbo.Subjects", new[] { "StudentSubject_Id" });
            DropColumn("dbo.Subjects", "StudentSubject_Id");
        }
    }
}
