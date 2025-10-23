﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaillotStore.Migrations
{
    /// <inheritdoc />
    public partial class AddCommissionSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CommissionSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateSet = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommissionSettings");

            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Orders");
        }
    }
}
