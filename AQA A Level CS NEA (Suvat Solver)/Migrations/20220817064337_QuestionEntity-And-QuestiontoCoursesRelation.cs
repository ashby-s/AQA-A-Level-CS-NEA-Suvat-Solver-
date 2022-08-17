using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Migrations
{
    public partial class QuestionEntityAndQuestiontoCoursesRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Question",
               columns: table => new
               {
                   QuestionId = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   AnswType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                   AnswTrue = table.Column<float>(type: "real", nullable: false),
                   QuestDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   QuestSolved = table.Column<string>(type: "nvarchar(max)", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Question", x => x.QuestionId);
               });

            migrationBuilder.CreateTable(
                name: "QuestiontoCourses",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestiontoCourses", x => new { x.QuestionId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_QuestiontoCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestiontoCourses_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestiontoCourses_CourseId",
                table: "QuestiontoCourses",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestiontoCourses");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
