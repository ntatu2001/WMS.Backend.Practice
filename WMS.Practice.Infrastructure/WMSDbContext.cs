namespace WMS.Practice.Infrastructure
{
    public class WMSDbContext : DbContext, IUnitOfWork
    {
        // Aggregate Models - Person Aggregate
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<EmployeeClass> EmployeeClasses { get; set; }
        public DbSet<EmployeeClassProperty> EmployeeClassProperties { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeProperty> EmployeeProperties { get; set; }

        // Aggregate Models - Storage Aggregate
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationProperty> LocationsProperties { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseProperty> WarehouseProperties { get; set; }

        // Aggregate Models - Material Aggregate
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialProperty> MaterialProperties { get; set; }
        public DbSet<MaterialClass> MaterialClasses { get; set; }
        public DbSet<MaterialClassProperty> MaterialClassProperties { get; set; }
        public DbSet<MaterialLot> MaterialLots { get; set; }
        public DbSet<MaterialLotProperty> MaterialLotProperties { get; set; }
        public DbSet<MaterialSubLot> MaterialSubLots { get; set; }

        // Aggregate Models - Inventory Receipt Aggregate
        public DbSet<InventoryReceipt> InventoryReceipts { get; set; }
        public DbSet<InventoryReceiptEntry> InventoryReceiptEntries { get; set; }
        public DbSet<ReceiptLot> ReceiptLots { get; set; }
        public DbSet<ReceiptSubLot> ReceiptSubLots { get; set; }

        // Aggregate Models - Inventory Issue Aggregate
        public DbSet<InventoryIssue> InventoryIssues { get; set; }
        public DbSet<InventoryIssueEntry> InventoryIssueEntries { get; set; }
        public DbSet<IssueLot> IssueLots { get; set; }
        public DbSet<IssueSubLot> IssueSubLots { get; set; }

        // Aggregate Models - Stock Take Aggregate
        public DbSet<StockTake> StockTakes { get; set; }
        public DbSet<StockTakeSubLot> StockTakeSubLots { get; set; }

        // Aggregate Models - Inventory Log Aggregate
        public DbSet<InventoryLog> InventoryLogs { get; set; }

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
