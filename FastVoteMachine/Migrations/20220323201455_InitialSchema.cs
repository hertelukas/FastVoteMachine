using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastVoteMachine.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "connections",
                columns: table => new
                {
                    ConnectionEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Connection = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_connections", x => x.ConnectionEntityId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "votes",
                columns: table => new
                {
                    VoteEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_votes", x => x.VoteEntityId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConnectionEntityVoteEntity",
                columns: table => new
                {
                    ConnectionsConnectionEntityId = table.Column<int>(type: "int", nullable: false),
                    VotesVoteEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionEntityVoteEntity", x => new { x.ConnectionsConnectionEntityId, x.VotesVoteEntityId });
                    table.ForeignKey(
                        name: "FK_ConnectionEntityVoteEntity_connections_ConnectionsConnection~",
                        column: x => x.ConnectionsConnectionEntityId,
                        principalTable: "connections",
                        principalColumn: "ConnectionEntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionEntityVoteEntity_votes_VotesVoteEntityId",
                        column: x => x.VotesVoteEntityId,
                        principalTable: "votes",
                        principalColumn: "VoteEntityId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    OptionEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VoteEntityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Votes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => x.OptionEntityId);
                    table.ForeignKey(
                        name: "FK_options_votes_VoteEntityId",
                        column: x => x.VoteEntityId,
                        principalTable: "votes",
                        principalColumn: "VoteEntityId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionEntityVoteEntity_VotesVoteEntityId",
                table: "ConnectionEntityVoteEntity",
                column: "VotesVoteEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_options_VoteEntityId",
                table: "options",
                column: "VoteEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionEntityVoteEntity");

            migrationBuilder.DropTable(
                name: "options");

            migrationBuilder.DropTable(
                name: "connections");

            migrationBuilder.DropTable(
                name: "votes");
        }
    }
}
