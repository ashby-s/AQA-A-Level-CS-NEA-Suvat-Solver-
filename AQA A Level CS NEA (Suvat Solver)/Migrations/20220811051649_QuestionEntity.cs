using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Migrations
{
    public partial class QuestionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    AnswTrue = table.Column<float>(type: "real", nullable: false),
                    QuestDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestSolved = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
