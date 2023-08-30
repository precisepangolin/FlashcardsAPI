using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlashcardsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flashcards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FrontSide = table.Column<string>(type: "text", nullable: false),
                    BackSide = table.Column<string>(type: "text", nullable: false),
                    IsMastered = table.Column<bool>(type: "boolean", nullable: false),
                    FolderId = table.Column<int>(type: "integer", nullable: false),
                    FlashcardFolderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flashcards_Folders_FlashcardFolderId",
                        column: x => x.FlashcardFolderId,
                        principalTable: "Folders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Flashcards",
                columns: new[] { "Id", "BackSide", "FlashcardFolderId", "FolderId", "FrontSide", "IsMastered" },
                values: new object[,]
                {
                    { 1, "Rewers1", null, 1, "Awers1", false },
                    { 2, "Rewers2", null, 1, "Awers2", false },
                    { 3, "Rewers3", null, 2, "Awers3", false },
                    { 4, "Rewers4", null, 3, "Awers4", false }
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PierwszyFolder" },
                    { 2, "DrugiFolder" },
                    { 3, "TrzeciFolder" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_FlashcardFolderId",
                table: "Flashcards",
                column: "FlashcardFolderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flashcards");

            migrationBuilder.DropTable(
                name: "Folders");
        }
    }
}
