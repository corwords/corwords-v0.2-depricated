using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Corwords.Web.Migrations
{
    public partial class AddTagsAndBlogTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corwords_Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corwords_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Corwords_BlogTag",
                columns: table => new
                {
                    BlogTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corwords_BlogTag", x => x.BlogTagId);
                    table.ForeignKey(
                        name: "FK_Corwords_BlogTag_Corwords_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Corwords_Blog",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corwords_BlogTag_Corwords_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Corwords_Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Corwords_BlogTag_BlogId",
                table: "Corwords_BlogTag",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Corwords_BlogTag_TagId",
                table: "Corwords_BlogTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Corwords_BlogTag");

            migrationBuilder.DropTable(
                name: "Corwords_Tag");
        }
    }
}
