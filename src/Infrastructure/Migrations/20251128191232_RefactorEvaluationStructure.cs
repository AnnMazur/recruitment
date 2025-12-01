using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEvaluationStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationCriteria_EvaluationForms_EvaluationFormId",
                table: "EvaluationCriteria");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationCriteria_EvaluationFormId",
                table: "EvaluationCriteria");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "EvaluationForms");

            migrationBuilder.DropColumn(
                name: "EvaluationFormId",
                table: "EvaluationCriteria");

            migrationBuilder.AddColumn<string>(
                name: "OverallComment",
                table: "EvaluationForms",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EvaluationCriteria",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxScore",
                table: "EvaluationCriteria",
                type: "integer",
                nullable: false,
                defaultValue: 10);

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "EvaluationCriteria",
                type: "jsonb",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CriterionScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluationFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriterionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    NumericValue = table.Column<double>(type: "numeric(3,1)", nullable: false),
                    Comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriterionScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriterionScores_EvaluationCriteria_CriterionId",
                        column: x => x.CriterionId,
                        principalTable: "EvaluationCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CriterionScores_EvaluationForms_EvaluationFormId",
                        column: x => x.EvaluationFormId,
                        principalTable: "EvaluationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriterionScores_CriterionId",
                table: "CriterionScores",
                column: "CriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionScores_EvaluationFormId",
                table: "CriterionScores",
                column: "EvaluationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionScores_NumericValue",
                table: "CriterionScores",
                column: "NumericValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriterionScores");

            migrationBuilder.DropColumn(
                name: "OverallComment",
                table: "EvaluationForms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "EvaluationCriteria");

            migrationBuilder.DropColumn(
                name: "MaxScore",
                table: "EvaluationCriteria");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "EvaluationCriteria");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "EvaluationForms",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EvaluationFormId",
                table: "EvaluationCriteria",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_EvaluationFormId",
                table: "EvaluationCriteria",
                column: "EvaluationFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationCriteria_EvaluationForms_EvaluationFormId",
                table: "EvaluationCriteria",
                column: "EvaluationFormId",
                principalTable: "EvaluationForms",
                principalColumn: "Id");
        }
    }
}
