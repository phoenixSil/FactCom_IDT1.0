using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idt.Data.Migrations
{
    public partial class builddatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    PseudoUtilisateur = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    DateDeCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateDeModification = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UtilisateurId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Pays = table.Column<string>(type: "TEXT", nullable: false),
                    Region = table.Column<string>(type: "TEXT", nullable: false),
                    Ville = table.Column<string>(type: "TEXT", nullable: false),
                    Telephone = table.Column<int>(type: "INTEGER", nullable: false),
                    Cellulaire = table.Column<int>(type: "INTEGER", nullable: false),
                    EstAdressePrincipale = table.Column<bool>(type: "INTEGER", nullable: false),
                    UtilisateurId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateDeCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateDeModification = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresses_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    UtilisateurId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EstLue = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateDeCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateDeModification = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_UtilisateurId",
                table: "Adresses",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UtilisateurId",
                table: "Messages",
                column: "UtilisateurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
