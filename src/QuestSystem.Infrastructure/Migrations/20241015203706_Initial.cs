using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuestSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quest_requirement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    minimum_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quest_requirement", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "quest_reward",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    experience = table.Column<int>(type: "integer", nullable: false),
                    items = table.Column<List<string>>(type: "text[]", nullable: false),
                    currency = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quest_reward", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "quests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    reward_id = table.Column<int>(type: "integer", nullable: false),
                    requirement_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quests", x => x.id);
                    table.ForeignKey(
                        name: "fk_quests_quest_requirement_requirement_id",
                        column: x => x.requirement_id,
                        principalTable: "quest_requirement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quests_quest_reward_reward_id",
                        column: x => x.reward_id,
                        principalTable: "quest_reward",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quest_condition",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    aim = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    quest_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quest_condition", x => x.id);
                    table.ForeignKey(
                        name: "fk_quest_condition_quests_quest_id",
                        column: x => x.quest_id,
                        principalTable: "quests",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_quests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quest_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    got_reward = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_quests", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_quests_quests_quest_id",
                        column: x => x.quest_id,
                        principalTable: "quests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_quests_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "quest_condition_progress",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    condition_id = table.Column<int>(type: "integer", nullable: false),
                    progress = table.Column<int>(type: "integer", nullable: false),
                    user_quest_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quest_condition_progress", x => x.id);
                    table.ForeignKey(
                        name: "fk_quest_condition_progress_quest_condition_condition_id",
                        column: x => x.condition_id,
                        principalTable: "quest_condition",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quest_condition_progress_user_quests_user_quest_id",
                        column: x => x.user_quest_id,
                        principalTable: "user_quests",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "level", "name" },
                values: new object[] { new Guid("a8587ff3-432c-4d91-920e-d1d50c07558e"), 0, "Oleg" });

            migrationBuilder.CreateIndex(
                name: "ix_quest_condition_quest_id",
                table: "quest_condition",
                column: "quest_id");

            migrationBuilder.CreateIndex(
                name: "ix_quest_condition_progress_condition_id",
                table: "quest_condition_progress",
                column: "condition_id");

            migrationBuilder.CreateIndex(
                name: "ix_quest_condition_progress_user_quest_id",
                table: "quest_condition_progress",
                column: "user_quest_id");

            migrationBuilder.CreateIndex(
                name: "ix_quests_requirement_id",
                table: "quests",
                column: "requirement_id");

            migrationBuilder.CreateIndex(
                name: "ix_quests_reward_id",
                table: "quests",
                column: "reward_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_quests_quest_id",
                table: "user_quests",
                column: "quest_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_quests_user_id",
                table: "user_quests",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quest_condition_progress");

            migrationBuilder.DropTable(
                name: "quest_condition");

            migrationBuilder.DropTable(
                name: "user_quests");

            migrationBuilder.DropTable(
                name: "quests");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "quest_requirement");

            migrationBuilder.DropTable(
                name: "quest_reward");
        }
    }
}
