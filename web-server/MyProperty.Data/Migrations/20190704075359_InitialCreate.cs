using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProperty.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "owners",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<Guid>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    username = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "properties_owners",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<Guid>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    phone = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_properties_owners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tenants",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<Guid>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    phone = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "properties",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<Guid>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    property_owner_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: false),
                    rent = table.Column<string>(nullable: false),
                    area = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_properties", x => x.id);
                    table.ForeignKey(
                        name: "FK_properties_properties_owners_property_owner_id",
                        column: x => x.property_owner_id,
                        principalTable: "properties_owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assigned_properties",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<Guid>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<Guid>(nullable: false),
                    property_id = table.Column<Guid>(nullable: false),
                    rent = table.Column<string>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    RentStartDate = table.Column<DateTime>(nullable: false),
                    RentDocumentFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assigned_properties", x => x.id);
                    table.ForeignKey(
                        name: "FK_assigned_properties_properties_property_id",
                        column: x => x.property_id,
                        principalTable: "properties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assigned_properties_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assigned_property_histories",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<Guid>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<Guid>(nullable: false),
                    property_id = table.Column<Guid>(nullable: false),
                    rent = table.Column<string>(nullable: false),
                    date_from = table.Column<DateTime>(nullable: false),
                    date_to = table.Column<DateTime>(nullable: true),
                    RentStartDate = table.Column<DateTime>(nullable: false),
                    RentDocumentFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assigned_property_histories", x => x.id);
                    table.ForeignKey(
                        name: "FK_assigned_property_histories_properties_property_id",
                        column: x => x.property_id,
                        principalTable: "properties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assigned_property_histories_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<Guid>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    property_id = table.Column<Guid>(nullable: false),
                    property_owner_id = table.Column<Guid>(nullable: true),
                    tenant_id = table.Column<Guid>(nullable: true),
                    amount = table.Column<string>(nullable: false),
                    credit = table.Column<bool>(nullable: false),
                    debit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.id);
                    table.ForeignKey(
                        name: "FK_payments_properties_property_id",
                        column: x => x.property_id,
                        principalTable: "properties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payments_properties_owners_property_owner_id",
                        column: x => x.property_owner_id,
                        principalTable: "properties_owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payments_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assigned_properties_property_id",
                table: "assigned_properties",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_assigned_properties_tenant_id",
                table: "assigned_properties",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_assigned_property_histories_property_id",
                table: "assigned_property_histories",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_assigned_property_histories_tenant_id",
                table: "assigned_property_histories",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_property_id",
                table: "payments",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_property_owner_id",
                table: "payments",
                column: "property_owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_tenant_id",
                table: "payments",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_properties_property_owner_id",
                table: "properties",
                column: "property_owner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assigned_properties");

            migrationBuilder.DropTable(
                name: "assigned_property_histories");

            migrationBuilder.DropTable(
                name: "owners");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "properties");

            migrationBuilder.DropTable(
                name: "tenants");

            migrationBuilder.DropTable(
                name: "properties_owners");
        }
    }
}
