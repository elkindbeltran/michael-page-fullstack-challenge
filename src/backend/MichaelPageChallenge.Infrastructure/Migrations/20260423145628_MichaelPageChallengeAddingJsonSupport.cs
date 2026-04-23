using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MichaelPageChallenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MichaelPageChallengeAddingJsonSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalData",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalData",
                table: "Tasks");
        }
    }
}
