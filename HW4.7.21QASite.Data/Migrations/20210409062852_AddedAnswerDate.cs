using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HW4._7._21QASite.Data.Migrations
{
    public partial class AddedAnswerDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAnswered",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAnswered",
                table: "Answers");
        }
    }
}
