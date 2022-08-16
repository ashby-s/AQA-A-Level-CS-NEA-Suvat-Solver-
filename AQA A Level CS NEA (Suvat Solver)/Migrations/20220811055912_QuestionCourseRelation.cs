using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Migrations
{
    public partial class QuestionCourseRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestiontoCourses",
                columns: table => new
                {
                    QuestId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    QuestionQuestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestiontoCourses", x => new { x.QuestId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_QuestiontoCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestiontoCourses_Question_QuestionQuestId",
                        column: x => x.QuestionQuestId,
                        principalTable: "Question",
                        principalColumn: "QuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestiontoCourses_CourseId",
                table: "QuestiontoCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestiontoCourses_QuestionQuestId",
                table: "QuestiontoCourses",
                column: "QuestionQuestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestiontoCourses");
        }
    }
}
