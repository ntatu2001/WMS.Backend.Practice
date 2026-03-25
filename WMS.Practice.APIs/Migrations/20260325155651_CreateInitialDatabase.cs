using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Practice.APIs.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeClasses",
                columns: table => new
                {
                    EmployeeClassId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeClasses", x => x.EmployeeClassId);
                });

            migrationBuilder.CreateTable(
                name: "MaterialClasses",
                columns: table => new
                {
                    MaterialClassId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaterialClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialClasses", x => x.MaterialClassId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeClassProperties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeeClassId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeClassProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_EmployeeClassProperties_EmployeeClasses_EmployeeClassId",
                        column: x => x.EmployeeClassId,
                        principalTable: "EmployeeClasses",
                        principalColumn: "EmployeeClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeeClassId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeClasses_EmployeeClassId",
                        column: x => x.EmployeeClassId,
                        principalTable: "EmployeeClasses",
                        principalColumn: "EmployeeClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialClassProperties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialClassId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialClassProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_MaterialClassProperties_MaterialClasses_MaterialClassId",
                        column: x => x.MaterialClassId,
                        principalTable: "MaterialClasses",
                        principalColumn: "MaterialClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaterialClassId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialClasses_MaterialClassId",
                        column: x => x.MaterialClassId,
                        principalTable: "MaterialClasses",
                        principalColumn: "MaterialClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Locations_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseProperties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_WarehouseProperties_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProperties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitOfMeasure = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_EmployeeProperties_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryIssues",
                columns: table => new
                {
                    InventoryIssueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssueStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryIssues", x => x.InventoryIssueId);
                    table.ForeignKey(
                        name: "FK_InventoryIssues_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryIssues_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryIssues_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryReceipts",
                columns: table => new
                {
                    InventoryReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryReceipts", x => x.InventoryReceiptId);
                    table.ForeignKey(
                        name: "FK_InventoryReceipts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryReceipts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryReceipts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialLots",
                columns: table => new
                {
                    LotNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExistingQuantity = table.Column<double>(type: "float", nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialLots", x => x.LotNumber);
                    table.ForeignKey(
                        name: "FK_MaterialLots_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialProperties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_MaterialProperties_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationProperties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_LocationProperties_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryIssueEntries",
                columns: table => new
                {
                    InventoryIssueEntryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestedQuantity = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IssueLotId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryIssueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryIssueEntries", x => x.InventoryIssueEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryIssueEntries_InventoryIssues_InventoryIssueId",
                        column: x => x.InventoryIssueId,
                        principalTable: "InventoryIssues",
                        principalColumn: "InventoryIssueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryReceiptEntries",
                columns: table => new
                {
                    InventoryReceiptEntryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImportedQuantity = table.Column<double>(type: "float", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryReceiptEntries", x => x.InventoryReceiptEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryReceiptEntries_InventoryReceipts_InventoryReceiptId",
                        column: x => x.InventoryReceiptId,
                        principalTable: "InventoryReceipts",
                        principalColumn: "InventoryReceiptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryLogs",
                columns: table => new
                {
                    InventoryLogId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousQuantity = table.Column<double>(type: "float", nullable: false),
                    ChangedQuantity = table.Column<double>(type: "float", nullable: false),
                    AfterQuantity = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLogs", x => x.InventoryLogId);
                    table.ForeignKey(
                        name: "FK_InventoryLogs_MaterialLots_LotNumber",
                        column: x => x.LotNumber,
                        principalTable: "MaterialLots",
                        principalColumn: "LotNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryLogs_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialLotProperties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitOfMeasure = table.Column<int>(type: "int", nullable: false),
                    MaterialLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialLotProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_MaterialLotProperties_MaterialLots_MaterialLotId",
                        column: x => x.MaterialLotId,
                        principalTable: "MaterialLots",
                        principalColumn: "LotNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialSubLots",
                columns: table => new
                {
                    MaterialSubLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubLotStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExistingQuantity = table.Column<double>(type: "float", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialSubLots", x => x.MaterialSubLotId);
                    table.ForeignKey(
                        name: "FK_MaterialSubLots_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialSubLots_MaterialLots_LotNumber",
                        column: x => x.LotNumber,
                        principalTable: "MaterialLots",
                        principalColumn: "LotNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTakes",
                columns: table => new
                {
                    StockTakeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdjustmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviousQuantity = table.Column<double>(type: "float", nullable: false),
                    AdjustedQuantity = table.Column<double>(type: "float", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTakes", x => x.StockTakeId);
                    table.ForeignKey(
                        name: "FK_StockTakes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTakes_MaterialLots_LotNumber",
                        column: x => x.LotNumber,
                        principalTable: "MaterialLots",
                        principalColumn: "LotNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTakes_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueLots",
                columns: table => new
                {
                    IssueLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedQuantity = table.Column<double>(type: "float", nullable: false),
                    LotStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InventoryIssueEntryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLots", x => x.IssueLotId);
                    table.ForeignKey(
                        name: "FK_IssueLots_InventoryIssueEntries_InventoryIssueEntryId",
                        column: x => x.InventoryIssueEntryId,
                        principalTable: "InventoryIssueEntries",
                        principalColumn: "InventoryIssueEntryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLots_MaterialLots_MaterialLotId",
                        column: x => x.MaterialLotId,
                        principalTable: "MaterialLots",
                        principalColumn: "LotNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLots_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptLots",
                columns: table => new
                {
                    ReceiptLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportedQuantity = table.Column<double>(type: "float", nullable: false),
                    LotStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryReceiptEntryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptLots", x => x.ReceiptLotId);
                    table.ForeignKey(
                        name: "FK_ReceiptLots_InventoryReceiptEntries_InventoryReceiptEntryId",
                        column: x => x.InventoryReceiptEntryId,
                        principalTable: "InventoryReceiptEntries",
                        principalColumn: "InventoryReceiptEntryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptLots_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId");
                });

            migrationBuilder.CreateTable(
                name: "StockTakeSubLots",
                columns: table => new
                {
                    StockTakeSubLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialSubLotId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousQuantity = table.Column<double>(type: "float", nullable: false),
                    AdjustedQuantity = table.Column<double>(type: "float", nullable: false),
                    QuantityDifference = table.Column<double>(type: "float", nullable: false),
                    StockTakeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTakeSubLots", x => x.StockTakeSubLotId);
                    table.ForeignKey(
                        name: "FK_StockTakeSubLots_StockTakes_StockTakeId",
                        column: x => x.StockTakeId,
                        principalTable: "StockTakes",
                        principalColumn: "StockTakeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueSubLots",
                columns: table => new
                {
                    IssueSubLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedQuantity = table.Column<double>(type: "float", nullable: false),
                    MaterialSubLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IssueLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueSubLots", x => x.IssueSubLotId);
                    table.ForeignKey(
                        name: "FK_IssueSubLots_IssueLots_IssueLotId",
                        column: x => x.IssueLotId,
                        principalTable: "IssueLots",
                        principalColumn: "IssueLotId");
                    table.ForeignKey(
                        name: "FK_IssueSubLots_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK_IssueSubLots_MaterialSubLots_MaterialSubLotId",
                        column: x => x.MaterialSubLotId,
                        principalTable: "MaterialSubLots",
                        principalColumn: "MaterialSubLotId");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptSubLots",
                columns: table => new
                {
                    ReceiptSubLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportedQuantity = table.Column<double>(type: "float", nullable: false),
                    LotStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiptLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptSubLots", x => x.ReceiptSubLotId);
                    table.ForeignKey(
                        name: "FK_ReceiptSubLots_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK_ReceiptSubLots_ReceiptLots_ReceiptLotId",
                        column: x => x.ReceiptLotId,
                        principalTable: "ReceiptLots",
                        principalColumn: "ReceiptLotId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeClassProperties_EmployeeClassId",
                table: "EmployeeClassProperties",
                column: "EmployeeClassId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProperties_EmployeeId",
                table: "EmployeeProperties",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeClassId",
                table: "Employees",
                column: "EmployeeClassId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryIssueEntries_InventoryIssueId",
                table: "InventoryIssueEntries",
                column: "InventoryIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryIssues_CustomerId",
                table: "InventoryIssues",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryIssues_EmployeeId",
                table: "InventoryIssues",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryIssues_WarehouseId",
                table: "InventoryIssues",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_LotNumber",
                table: "InventoryLogs",
                column: "LotNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_WarehouseId",
                table: "InventoryLogs",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceiptEntries_InventoryReceiptId",
                table: "InventoryReceiptEntries",
                column: "InventoryReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceipts_EmployeeId",
                table: "InventoryReceipts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceipts_SupplierId",
                table: "InventoryReceipts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReceipts_WarehouseId",
                table: "InventoryReceipts",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLots_InventoryIssueEntryId",
                table: "IssueLots",
                column: "InventoryIssueEntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueLots_MaterialId",
                table: "IssueLots",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLots_MaterialLotId",
                table: "IssueLots",
                column: "MaterialLotId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSubLots_IssueLotId",
                table: "IssueSubLots",
                column: "IssueLotId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSubLots_LocationId",
                table: "IssueSubLots",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSubLots_MaterialSubLotId",
                table: "IssueSubLots",
                column: "MaterialSubLotId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationProperties_LocationId",
                table: "LocationProperties",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_WarehouseId",
                table: "Locations",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialClassProperties_MaterialClassId",
                table: "MaterialClassProperties",
                column: "MaterialClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLotProperties_MaterialLotId",
                table: "MaterialLotProperties",
                column: "MaterialLotId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLots_MaterialId",
                table: "MaterialLots",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialProperties_MaterialId",
                table: "MaterialProperties",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialClassId",
                table: "Materials",
                column: "MaterialClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialSubLots_LocationId",
                table: "MaterialSubLots",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialSubLots_LotNumber",
                table: "MaterialSubLots",
                column: "LotNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLots_InventoryReceiptEntryId",
                table: "ReceiptLots",
                column: "InventoryReceiptEntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLots_MaterialId",
                table: "ReceiptLots",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSubLots_LocationId",
                table: "ReceiptSubLots",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSubLots_ReceiptLotId",
                table: "ReceiptSubLots",
                column: "ReceiptLotId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTakes_EmployeeId",
                table: "StockTakes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTakes_LotNumber",
                table: "StockTakes",
                column: "LotNumber");

            migrationBuilder.CreateIndex(
                name: "IX_StockTakes_WarehouseId",
                table: "StockTakes",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTakeSubLots_StockTakeId",
                table: "StockTakeSubLots",
                column: "StockTakeId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProperties_WarehouseId",
                table: "WarehouseProperties",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeClassProperties");

            migrationBuilder.DropTable(
                name: "EmployeeProperties");

            migrationBuilder.DropTable(
                name: "InventoryLogs");

            migrationBuilder.DropTable(
                name: "IssueSubLots");

            migrationBuilder.DropTable(
                name: "LocationProperties");

            migrationBuilder.DropTable(
                name: "MaterialClassProperties");

            migrationBuilder.DropTable(
                name: "MaterialLotProperties");

            migrationBuilder.DropTable(
                name: "MaterialProperties");

            migrationBuilder.DropTable(
                name: "ReceiptSubLots");

            migrationBuilder.DropTable(
                name: "StockTakeSubLots");

            migrationBuilder.DropTable(
                name: "WarehouseProperties");

            migrationBuilder.DropTable(
                name: "IssueLots");

            migrationBuilder.DropTable(
                name: "MaterialSubLots");

            migrationBuilder.DropTable(
                name: "ReceiptLots");

            migrationBuilder.DropTable(
                name: "StockTakes");

            migrationBuilder.DropTable(
                name: "InventoryIssueEntries");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "InventoryReceiptEntries");

            migrationBuilder.DropTable(
                name: "MaterialLots");

            migrationBuilder.DropTable(
                name: "InventoryIssues");

            migrationBuilder.DropTable(
                name: "InventoryReceipts");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "MaterialClasses");

            migrationBuilder.DropTable(
                name: "EmployeeClasses");
        }
    }
}
