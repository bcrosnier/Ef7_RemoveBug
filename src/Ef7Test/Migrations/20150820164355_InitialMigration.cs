using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace Ef7TestMigrations
{
    public partial class InitialMigration : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    CompanyName = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });
            migration.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    CompanyId = table.Column(type: "int", nullable: false),
                    ContactName = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contact_Company_CompanyId",
                        columns: x => x.CompanyId,
                        referencedTable: "Company",
                        referencedColumn: "CompanyId");
                });
            migration.CreateTable(
                name: "ContactEmail",
                columns: table => new
                {
                    ContactId = table.Column(type: "int", nullable: false),
                    ContactEmailAddress = table.Column(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactEmail", x => new { x.ContactId, x.ContactEmailAddress });
                    table.ForeignKey(
                        name: "FK_ContactEmail_Contact_ContactId",
                        columns: x => x.ContactId,
                        referencedTable: "Contact",
                        referencedColumn: "ContactId");
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("ContactEmail");
            migration.DropTable("Contact");
            migration.DropTable("Company");
        }
    }
}
