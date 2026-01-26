using WMS.Practice.Domain.AggregateModels.InventoryIssueAggregate;
using WMS.Practice.Domain.AggregateModels.InventoryLogAggregate;
using WMS.Practice.Domain.AggregateModels.IssueReceiptAggregate;
using WMS.Practice.Domain.AggregateModels.MaterialAggregate;
using WMS.Practice.Domain.AggregateModels.PersonAggregate;
using WMS.Practice.Domain.AggregateModels.StockTakeAggregate;
using WMS.Practice.Domain.AggregateModels.StorageAggregate;

namespace WMS.Practice.Infrastructure
{
    public class WMSDbContext : DbContext, IUnitOfWork
    {
        // Aggregate Models - Person Aggregate
        DbSet<Customer> Customers { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<EmployeeClass> EmployeeClasses { get; set; }
        DbSet<EmployeeClassProperty> EmployeeClassProperties { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeProperty> EmployeeProperties { get; set; }

        // Aggregate Models - Storage Aggregate
        DbSet<Location> Locations { get; set; }
        DbSet<LocationProperty> LocationsProperties { get; set; }
        DbSet<Warehouse> Warehouses { get; set; }
        DbSet<WarehouseProperty> WarehouseProperties { get; set; }

        // Aggregate Models - Material Aggregate
        DbSet<Material> Materials { get; set; }
        DbSet<MaterialProperty> MaterialProperties { get; set; }
        DbSet<MaterialClass> MaterialClasses { get; set; }
        DbSet<MaterialClassProperty> MaterialClassProperties { get; set; }
        DbSet<MaterialLot> MaterialLots { get; set; }
        DbSet<MaterialLotProperty> MaterialLotProperties { get; set; }
        DbSet<MaterialSubLot> MaterialSubLots { get; set; }

        // Aggregate Models - Inventory Receipt Aggregate
        DbSet<InventoryReceipt> InventoryReceipts { get; set; }
        DbSet<InventoryReceiptEntry> InventoryReceiptEntries { get; set; }
        DbSet<ReceiptLot> ReceiptLots { get; set; }
        DbSet<ReceiptSubLot> ReceiptSubLots { get; set; }

        // Aggregate Models - Inventory Issue Aggregate
        DbSet<InventoryIssue> InventoryIssues { get; set; }
        DbSet<InventoryIssueEntry> InventoryIssueEntries { get; set; }
        DbSet<IssueLot> IssueLots { get; set; }
        DbSet<IssueSubLot> IssueSubLots { get; set; }

        // Aggregate Models - Stock Take Aggregate
        DbSet<StockTake> StockTakes { get; set; }
        DbSet<StockTakeSubLot> StockTakeSubLots { get; set; }

        // Aggregate Models - Inventory Log Aggregate
        DbSet<InventoryLog> InventoryLogs { get; set; }

        private readonly IMediator _mediator;
        public WMSDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
