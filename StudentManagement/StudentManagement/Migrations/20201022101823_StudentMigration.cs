using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Migrations
{
    public partial class StudentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Studid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Studname = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Subjects = table.Column<string>(nullable: true),
                    fees = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Studid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
