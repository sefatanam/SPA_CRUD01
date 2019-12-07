namespace SPA_Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubjectCollectionIsAdded3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubjectStudents", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectStudents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Subjects", "StudentSubject_Id", "dbo.StudentSubjects");
            DropIndex("dbo.Subjects", new[] { "StudentSubject_Id" });
            DropIndex("dbo.SubjectStudents", new[] { "Subject_Id" });
            DropIndex("dbo.SubjectStudents", new[] { "Student_Id" });
            CreateTable(
                "dbo.SubjectStudentSubjects",
                c => new
                    {
                        Subject_Id = c.Int(nullable: false),
                        StudentSubject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_Id, t.StudentSubject_Id })
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .ForeignKey("dbo.StudentSubjects", t => t.StudentSubject_Id, cascadeDelete: true)
                .Index(t => t.Subject_Id)
                .Index(t => t.StudentSubject_Id);
            
            CreateIndex("dbo.StudentSubjects", "StudentId");
            AddForeignKey("dbo.StudentSubjects", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            DropColumn("dbo.Subjects", "StudentSubject_Id");
            DropTable("dbo.SubjectStudents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubjectStudents",
                c => new
                    {
                        Subject_Id = c.Int(nullable: false),
                        Student_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_Id, t.Student_Id });
            
            AddColumn("dbo.Subjects", "StudentSubject_Id", c => c.Int());
            DropForeignKey("dbo.StudentSubjects", "StudentId", "dbo.Students");
            DropForeignKey("dbo.SubjectStudentSubjects", "StudentSubject_Id", "dbo.StudentSubjects");
            DropForeignKey("dbo.SubjectStudentSubjects", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SubjectStudentSubjects", new[] { "StudentSubject_Id" });
            DropIndex("dbo.SubjectStudentSubjects", new[] { "Subject_Id" });
            DropIndex("dbo.StudentSubjects", new[] { "StudentId" });
            DropTable("dbo.SubjectStudentSubjects");
            CreateIndex("dbo.SubjectStudents", "Student_Id");
            CreateIndex("dbo.SubjectStudents", "Subject_Id");
            CreateIndex("dbo.Subjects", "StudentSubject_Id");
            AddForeignKey("dbo.Subjects", "StudentSubject_Id", "dbo.StudentSubjects", "Id");
            AddForeignKey("dbo.SubjectStudents", "Student_Id", "dbo.Students", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubjectStudents", "Subject_Id", "dbo.Subjects", "Id", cascadeDelete: true);
        }
    }
}
