﻿using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace A13MovieLibrary.Migrations
{
    public partial class InsertGenresAndMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "SQL", @"5-InsertGenresAndMovies.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from moviegenres");
        }
    }
}