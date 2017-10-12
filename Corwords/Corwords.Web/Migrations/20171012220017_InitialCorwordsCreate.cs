using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Corwords.Web.Migrations
{
    public partial class InitialCorwordsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corwords_Blog",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corwords_Blog", x => x.BlogId);
                });

            migrationBuilder.CreateTable(
                name: "Corwords_RouteFact",
                columns: table => new
                {
                    RouteFactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDiscontinued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForwardTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteType = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corwords_RouteFact", x => x.RouteFactId);
                });

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
                name: "Corwords_BlogPost",
                columns: table => new
                {
                    BlogPostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalBlogPostId = table.Column<int>(type: "int", nullable: true),
                    Permalink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corwords_BlogPost", x => x.BlogPostId);
                    table.ForeignKey(
                        name: "FK_Corwords_BlogPost_Corwords_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Corwords_Blog",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Corwords_BlogPostTag",
                columns: table => new
                {
                    BlogPostTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corwords_BlogPostTag", x => x.BlogPostTagId);
                    table.ForeignKey(
                        name: "FK_Corwords_BlogPostTag_Corwords_BlogPost_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "Corwords_BlogPost",
                        principalColumn: "BlogPostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corwords_BlogPostTag_Corwords_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Corwords_Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Corwords_BlogPost_BlogId",
                table: "Corwords_BlogPost",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Corwords_BlogPostTag_BlogPostId",
                table: "Corwords_BlogPostTag",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Corwords_BlogPostTag_TagId",
                table: "Corwords_BlogPostTag",
                column: "TagId");

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
                name: "Corwords_BlogPostTag");

            migrationBuilder.DropTable(
                name: "Corwords_BlogTag");

            migrationBuilder.DropTable(
                name: "Corwords_RouteFact");

            migrationBuilder.DropTable(
                name: "Corwords_BlogPost");

            migrationBuilder.DropTable(
                name: "Corwords_Tag");

            migrationBuilder.DropTable(
                name: "Corwords_Blog");
        }
    }
}
