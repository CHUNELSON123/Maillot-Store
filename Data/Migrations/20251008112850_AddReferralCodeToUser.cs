﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaillotStore.Migrations
{
    /// <inheritdoc />
    public partial class AddReferralCodeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferralCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferralCode",
                table: "AspNetUsers");
        }
    }
}
