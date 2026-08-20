namespace WMS.Practice.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Retrieve the connection string from the configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add the DbContext to the service container with the connection string
            builder.Services.AddDbContext<WMSDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("WMS.Practice.APIs")));

            // Add JWT settings and ASP.NET Core Identity backed by WMSDbContext
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            builder.Services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<WMSDbContext>()
                .AddDefaultTokenProviders();

            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
                ?? throw new InvalidOperationException("Jwt configuration section is missing.");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            builder.Services.AddScoped<ITokenService, TokenService>();

            // Add AutoMapper to the service container and specify the assembly where the profiles are located
            builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile).Assembly);

            // Add MediatR to the service container and specify the assembly where the handlers are located
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeePropertyRepository, EmployeePropertyRepository>();
            builder.Services.AddScoped<IEmployeeClassRepository, EmployeeClassRepository>();
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
            builder.Services.AddScoped<IStockLocationHistoryRepository, StockLocationHistoryRepository>();

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
            builder.Services.AddScoped<ILocationCapacityService, LocationCapacityService>();
            builder.Services.AddScoped<IOverviewService, OverviewService>();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                await IdentitySeeder.SeedAsync(scope.ServiceProvider);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}


