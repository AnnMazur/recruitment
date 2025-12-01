using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixInterviewRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationForms_Interviews_InterviewId",
                table: "EvaluationForms");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Interviews");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationForms_Interviews_InterviewId",
                table: "EvaluationForms",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationForms_Interviews_InterviewId",
                table: "EvaluationForms");

            migrationBuilder.AddColumn<Guid>(
                name: "FormId",
                table: "Interviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationForms_Interviews_InterviewId",
                table: "EvaluationForms",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
