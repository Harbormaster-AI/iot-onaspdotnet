using iotonaspdotnet.Api.Accounts;
using iotonaspdotnet.Api.Banks;
using iotonaspdotnet.Api.Customers;
using iotonaspdotnet.Service.Accounts;
using iotonaspdotnet.Service.Banks;
using iotonaspdotnet.Service.Customers;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.Accounts;
using iotonaspdotnet.Persistence.Banks;
using iotonaspdotnet.Persistence.Customers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

// MySQL for local/runtime; SQLite is used automatically when environment is Testing.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Tests set environment "Testing" and use SQLite so `dotnet test` needs no MySQL.
    if (builder.Environment.IsEnvironment("Testing"))
    {
        var sqlitePath = Path.Combine(Path.GetTempPath(), "iotonaspdotnet-testing.db");
        options.UseSqlite($"Data Source={sqlitePath}");
    }
    else
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
        options.UseMySql(connectionString, ServerVersion.Parse("8.4.0-mysql"));
    }
});

builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecks("/health");

app.MapBankEndpoints();
app.MapCustomerEndpoints();
app.MapAccountEndpoints();

app.Run();

public partial class Program { }
