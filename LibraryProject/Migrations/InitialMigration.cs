// <copyright file="InitialMigration.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace LibraryProject.Migrations
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// Initial Migration class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOMENII = table.Column<int>(type: "int", nullable: true),
                    NMC = table.Column<int>(type: "int", nullable: true),
                    L = table.Column<int>(type: "int", nullable: true),
                    PER = table.Column<int>(type: "int", nullable: true),
                    C = table.Column<int>(type: "int", nullable: true),
                    D = table.Column<int>(type: "int", nullable: true),
                    LIM = table.Column<int>(type: "int", nullable: true),
                    DELTA = table.Column<int>(type: "int", nullable: true),
                    NCZ = table.Column<int>(type: "int", nullable: true),
                    PERSIMP = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Properties", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReader = table.Column<bool>(type: "bit", nullable: true),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Readers", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_Readers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "Borrow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfTimeExtended = table.Column<int>(type: "int", nullable: false),
                    LibrarianId = table.Column<int>(type: "int", nullable: true),
                    ReaderId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Borrow", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_Borrow_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_Borrow_Readers_LibrarianId",
                        column: x => x.LibrarianId,
                        principalTable: "Readers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            _ = migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LecturesOnlyBook = table.Column<bool>(type: "bit", nullable: false),
                    IsBorrowed = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Books", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_Books_Borrow_BorrowId",
                        column: x => x.BorrowId,
                        principalTable: "Borrow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            _ = migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Authors", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_Authors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            _ = migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParentDomainId = table.Column<int>(type: "int", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Domains", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_Domains_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    _ = table.ForeignKey(
                        name: "FK_Domains_Domains_ParentDomainId",
                        column: x => x.ParentDomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            _ = migrationBuilder.CreateTable(
                name: "Editions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publisher = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    EditionNumber = table.Column<int>(type: "int", nullable: false),
                    NumberOfPages = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Editions", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_Editions_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_Authors_BookId",
                table: "Authors",
                column: "BookId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowId",
                table: "Books",
                column: "BorrowId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Borrow_ReaderId",
                table: "Borrow",
                column: "ReaderId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Borrow_LibrarianId",
                table: "Borrow",
                column: "LibrarianId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Readers_AccountId",
                table: "Readers",
                column: "AccountId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Domains_BookId",
                table: "Domains",
                column: "BookId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Domains_ParentDomainId",
                table: "Domains",
                column: "ParentDomainId");

            _ = migrationBuilder.CreateIndex(
                name: "IX_Editions_BookId",
                table: "Editions",
                column: "BookId");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "Authors");

            _ = migrationBuilder.DropTable(
                name: "Domains");

            _ = migrationBuilder.DropTable(
                name: "Editions");

            _ = migrationBuilder.DropTable(
                name: "Properties");

            _ = migrationBuilder.DropTable(
                name: "Books");

            _ = migrationBuilder.DropTable(
                name: "Borrow");

            _ = migrationBuilder.DropTable(
                name: "Readers");

            _ = migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
