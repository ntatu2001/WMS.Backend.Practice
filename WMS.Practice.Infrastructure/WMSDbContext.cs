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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply EF Configurations for Storage Aggregate
            modelBuilder.ApplyConfiguration(new LocationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocationPropertyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WarehousePropertyEntityTypeConfiguration());

            // Apply EF Configurations for Person Aggregate
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeClassEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeClassPropertyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeePropertyEntityTypeConfiguration());

            // Apply EF Configurations for Material Aggregate
            modelBuilder.ApplyConfiguration(new MaterialEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialPropertyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialClassEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialClassPropertyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialLotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialLotPropertyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialSubLotEntityTypeConfiguration());

            // Apply EF Configurations for Inventory Receipt Aggregate
            modelBuilder.ApplyConfiguration(new InventoryReceiptEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryReceiptEntryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptLotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptSubLotEntityTypeConfiguration());

            // Apply EF Configurations for Inventory Issue Aggregate
            modelBuilder.ApplyConfiguration(new InventoryIssueEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryIssueEntryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IssueLotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IssueSubLotEntityTypeConfiguration());

            // Apply EF Configurations for Stock Take Aggregate
            modelBuilder.ApplyConfiguration(new StockTakeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StockTakeSubLotEntityTypeConfiguration());

            // Apply EF Configurations for Inventory Log Aggregate
            modelBuilder.ApplyConfiguration(new InventoryLogEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
