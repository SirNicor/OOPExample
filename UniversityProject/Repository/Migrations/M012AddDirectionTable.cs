using System.Runtime.Intrinsics.X86;
using FluentMigrator;

namespace Repository.Migrations;
using Migrations;

[Migration(12, "Create new table Direction")]  
public class M012AddDirectionTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Direction")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("IdDepartment").AsInt32().NotNullable().ForeignKey("University", "Id")
            .WithColumn("IdDegreesStudy").AsInt32().NotNullable().ForeignKey("DegreesStudy", "Id")
            .WithColumn("NameDirection").AsString(100).NotNullable();
        Create.Table("StudentOfDirection")
            .WithColumn("IdDirection").AsInt32().ForeignKey("Direction", "Id")
            .WithColumn("IdStudent").AsInt32().ForeignKey("Student", "Id");
        Insert.IntoTable("Direction")
            .Row(new {IdDepartment = 1, IdDegreesStudy = 1, NameDirection = "Direction1"});
        for(int i = 1; i<11; i++)
        {
            Insert.IntoTable("StudentOfDirection")
                .Row(new { IdDirection = 1, IdStudent = i });
        }
    }
}