using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemplateAPI.Migrations
{
    public partial class Tabulky : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthCardId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthCard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Insurance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Illness",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illness", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Symptom",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthCardIllnesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HealthCardId = table.Column<int>(nullable: false),
                    IllnessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCardIllnesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthCardIllnesses_HealthCard_HealthCardId",
                        column: x => x.HealthCardId,
                        principalTable: "HealthCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthCardIllnesses_Illness_IllnessId",
                        column: x => x.IllnessId,
                        principalTable: "Illness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IllnessSymptoms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IllnessId = table.Column<int>(nullable: false),
                    SymptomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IllnessSymptoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IllnessSymptoms_Illness_IllnessId",
                        column: x => x.IllnessId,
                        principalTable: "Illness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IllnessSymptoms_Symptom_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_DoctorId",
                table: "User",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_User_HealthCardId",
                table: "User",
                column: "HealthCardId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthCardIllnesses_HealthCardId",
                table: "HealthCardIllnesses",
                column: "HealthCardId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthCardIllnesses_IllnessId",
                table: "HealthCardIllnesses",
                column: "IllnessId");

            migrationBuilder.CreateIndex(
                name: "IX_IllnessSymptoms_IllnessId",
                table: "IllnessSymptoms",
                column: "IllnessId");

            migrationBuilder.CreateIndex(
                name: "IX_IllnessSymptoms_SymptomId",
                table: "IllnessSymptoms",
                column: "SymptomId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Doctor_DoctorId",
                table: "User",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_HealthCard_HealthCardId",
                table: "User",
                column: "HealthCardId",
                principalTable: "HealthCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Doctor_DoctorId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_HealthCard_HealthCardId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "HealthCardIllnesses");

            migrationBuilder.DropTable(
                name: "IllnessSymptoms");

            migrationBuilder.DropTable(
                name: "HealthCard");

            migrationBuilder.DropTable(
                name: "Illness");

            migrationBuilder.DropTable(
                name: "Symptom");

            migrationBuilder.DropIndex(
                name: "IX_User_DoctorId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_HealthCardId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HealthCardId",
                table: "User");
        }
    }
}
