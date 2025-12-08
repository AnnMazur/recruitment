using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEvaluationStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EvaluationCriteria_Name",
                table: "EvaluationCriteria");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationCriteria_Type",
                table: "EvaluationCriteria");

            migrationBuilder.DropIndex(
                name: "IX_CriterionScores_NumericValue",
                table: "CriterionScores");

            migrationBuilder.DropColumn(
                name: "MaxScore",
                table: "EvaluationCriteria");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "EvaluationCriteria");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EvaluationCriteria");

            migrationBuilder.DropColumn(
                name: "NumericValue",
                table: "CriterionScores");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "CriterionScores");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "CriterionScores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_Name",
                table: "EvaluationCriteria",
                column: "Name",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_CriterionScore_Score_Range",
                table: "CriterionScores",
                sql: "\"Score\" >= 1 AND \"Score\" <= 10");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EvaluationCriteria_Name",
                table: "EvaluationCriteria");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CriterionScore_Score_Range",
                table: "CriterionScores");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "CriterionScores");

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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "EvaluationCriteria",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "NumericValue",
                table: "CriterionScores",
                type: "numeric(3,1)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "CriterionScores",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_Name",
                table: "EvaluationCriteria",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_Type",
                table: "EvaluationCriteria",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_CriterionScores_NumericValue",
                table: "CriterionScores",
                column: "NumericValue");
        }
    }
}
