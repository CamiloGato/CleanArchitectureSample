using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeinStreamers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Streamers_StreamerId",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Streamers_StreamerId",
                table: "Videos",
                column: "StreamerId",
                principalTable: "Streamers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Streamers_StreamerId",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Streamers_StreamerId",
                table: "Videos",
                column: "StreamerId",
                principalTable: "Streamers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
