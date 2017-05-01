using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreServer.Migrations
{
    public partial class ImageForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Images_ImageId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Images_ImageId",
                table: "SubCategories");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "SubCategories",
                newName: "ImageID");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategories_ImageId",
                table: "SubCategories",
                newName: "IX_SubCategories_ImageID");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Categories",
                newName: "ImageID");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ImageId",
                table: "Categories",
                newName: "IX_Categories_ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Images_ImageID",
                table: "Categories",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Images_ImageID",
                table: "SubCategories",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Images_ImageID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Images_ImageID",
                table: "SubCategories");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "SubCategories",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategories_ImageID",
                table: "SubCategories",
                newName: "IX_SubCategories_ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "Categories",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ImageID",
                table: "Categories",
                newName: "IX_Categories_ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Images_ImageId",
                table: "Categories",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Images_ImageId",
                table: "SubCategories",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
