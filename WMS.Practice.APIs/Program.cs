namespace WMS.Practice.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Retrieve the connection string from the configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<WMSDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("WMS.Practice.APIs")));

            builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile).Assembly);

            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeePropertyRepository, EmployeePropertyRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

            builder.Services.AddScoped<ILocationRepository, LocationRepository>();
            builder.Services.AddScoped<ILocationPropertyRepository, LocationPropertyRepository>();
            builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddScoped<IWarehousePropertyRepository, WarehousePropertyRepository>();

            builder.Services.AddScoped<IMaterialClassRepository, MaterialClassRepository>();
            builder.Services.AddScoped<IMaterialClassPropertyRepository, MaterialClassPropertyRepository>();
            builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
            builder.Services.AddScoped<IMaterialPropertyRepository, MaterialPropertyRepository>();
            builder.Services.AddScoped<IMaterialLotRepository, MaterialLotRepository>();
            builder.Services.AddScoped<IMaterialLotPropertyRepository, MaterialLotPropertyRepository>();
            builder.Services.AddScoped<IMaterialSubLotRepository, MaterialSubLotRepository>();

            builder.Services.AddScoped<IInventoryReceiptRepository, InventoryReceiptRepository>();
            builder.Services.AddScoped<IInventoryReceiptEntryRepository, InventoryReceiptEntryRepository>();
            builder.Services.AddScoped<IReceiptLotRepository, ReceiptLotRepository>();
            builder.Services.AddScoped<IReceiptSubLotRepository, ReceiptSubLotRepository>();

            builder.Services.AddScoped<IInventoryIssueRepository, InventoryIssueRepository>();
            builder.Services.AddScoped<IInventoryIssueEntryRepository, InventoryIssueEntryRepository>();
            builder.Services.AddScoped<IIssueLotRepository, IssueLotRepository>();
            builder.Services.AddScoped<IIssueSubLotRepository, IssueSubLotRepository>();

            builder.Services.AddScoped<IInventoryLogRepository, InventoryLogRepository>();

            builder.Services.AddScoped<IStockTakeRepository, StockTakeRepository>();
            builder.Services.AddScoped<IStockTakeSubLotRepository, StockTakeSubLotRepository>();

            builder.Services.AddScoped<IIssueLoggingService, IssueLoggingService>();
            builder.Services.AddScoped<IReceiptLoggingService, ReceiptLoggingService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


