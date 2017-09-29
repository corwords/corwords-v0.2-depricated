using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Corwords.Web.Migrations
{
    public partial class CreateBlogPostBlogTagUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corwords_BlogPost",
                columns: table => new
                {
                    BlogPostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Permalink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corwords_BlogPost", x => x.BlogPostId);
                });

            migrationBuilder.CreateTable(
                name: "Corwords_BlogPostBlogTag",
                columns: table => new
                {
                    BlogPostBlogTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    BlogTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corwords_BlogPostBlogTag", x => x.BlogPostBlogTagId);
                    table.ForeignKey(
                        name: "FK_Corwords_BlogPostBlogTag_Corwords_BlogPost_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "Corwords_BlogPost",
                        principalColumn: "BlogPostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corwords_BlogPostBlogTag_Corwords_BlogTag_BlogTagId",
                        column: x => x.BlogTagId,
                        principalTable: "Corwords_BlogTag",
                        principalColumn: "BlogTagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Corwords_BlogPostBlogTag_BlogPostId",
                table: "Corwords_BlogPostBlogTag",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Corwords_BlogPostBlogTag_BlogTagId",
                table: "Corwords_BlogPostBlogTag",
                column: "BlogTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Corwords_BlogPostBlogTag");

            migrationBuilder.DropTable(
                name: "Corwords_BlogPost");
        }
    }
}
