using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCharacterAndAddCampaignCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignActions_Users_ActorId",
                table: "CampaignActions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UndeadRaised",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "MainStat",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerClass",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CampaignCode",
                table: "Campaigns",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignActions_Characters_ActorId",
                table: "CampaignActions",
                column: "ActorId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignActions_Characters_ActorId",
                table: "CampaignActions");

            migrationBuilder.DropColumn(
                name: "MainStat",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PlayerClass",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "CampaignCode",
                table: "Campaigns");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Characters",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UndeadRaised",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignActions_Users_ActorId",
                table: "CampaignActions",
                column: "ActorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
