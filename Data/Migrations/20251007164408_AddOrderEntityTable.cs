using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaillotStore.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderEntityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OptionalMessage",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Orders",
                newName: "CustomerPhone");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CustomerPhone",
                table: "Orders",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionalMessage",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
