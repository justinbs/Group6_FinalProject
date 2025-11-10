using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
              ?? "Server=(localdb)\\MSSQLLocalDB;Database=Group6_FinalProject;Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connStr));

// CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// DI
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IReportService, ReportService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---- DEV: ALWAYS drop and recreate to avoid stale schemas ----
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    app.Logger.LogWarning("Using connection string: {Conn}", connStr);

    if (app.Environment.IsDevelopment())
    {
        app.Logger.LogWarning("DEV mode: Ensuring database is FRESH (EnsureDeleted + EnsureCreated)...");
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
    else
    {
        db.Database.Migrate();
    }

    await DbSeeder.SeedAsync(db);
}
// --------------------------------------------------------------

app.UseCors("AllowAll");
app.MapControllers();
app.MapGet("/healthz", () => Results.Ok(new { ok = true }));

app.Run();
