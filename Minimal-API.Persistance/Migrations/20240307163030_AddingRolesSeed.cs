using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Minimal_API.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddingRolesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedUtc", "IsDeleted", "ModifiedUtc", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("344422fd-b7c4-4aef-b18f-641549cb603e"), new DateTime(2024, 3, 7, 12, 0, 0, 0, DateTimeKind.Utc), false, null, "User", "USER" },
                    { new Guid("44904e59-04c1-4b72-b54c-d2796c842f09"), new DateTime(2024, 3, 7, 12, 0, 0, 0, DateTimeKind.Utc), false, null, "Admin", "ADMIN" },
                    { new Guid("f1a229c1-4059-4c72-85b9-9eab5aece972"), new DateTime(2024, 3, 7, 12, 0, 0, 0, DateTimeKind.Utc), false, null, "SuperAdmin", "SUPERADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("344422fd-b7c4-4aef-b18f-641549cb603e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44904e59-04c1-4b72-b54c-d2796c842f09"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f1a229c1-4059-4c72-85b9-9eab5aece972"));
        }
    }
}
