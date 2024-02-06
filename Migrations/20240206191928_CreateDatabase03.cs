﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRABALHO_volvo.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataManutencao",
                table: "ServicosManutencao",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataManutencao",
                table: "ServicosManutencao",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
