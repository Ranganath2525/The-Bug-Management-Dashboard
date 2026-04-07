using BugManagementAPI.Data;
using BugManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQLite
builder.Services.AddDbContext<BugDbContext>(options =>
    options.UseSqlite("Data Source=bugs.db"));

// Dependency Injection
builder.Services.AddScoped<IBugService, BugService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 👇 ADD THESE HERE
app.UseDefaultFiles();
app.UseStaticFiles();


// Ensure Database is Created and Seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BugDbContext>();
    context.Database.EnsureCreated();
    if (!context.Bugs.Any())
    {
        context.Bugs.AddRange(
            new BugManagementAPI.Models.Bug { Title = "Initial Bug", Description = "Pre-seeded for demo", Status = BugManagementAPI.Models.BugStatus.Open },
            new BugManagementAPI.Models.Bug { Title = "Critical Issue", Description = "Database connection slow", Status = BugManagementAPI.Models.BugStatus.WorkInProgress },
            new BugManagementAPI.Models.Bug { Title = "UI Alignment", Description = "Navbar shifts on resize", Status = BugManagementAPI.Models.BugStatus.Hold },
            new BugManagementAPI.Models.Bug { Title = "Feature Request", Description = "Export to PDF", Status = BugManagementAPI.Models.BugStatus.Closed }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Disabled for demo access

app.UseCors("AllowAngular");

// Global Error Handling Middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "An internal server error occurred.", message = ex.Message });
    }
});

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Content(@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Bug Management Dashboard - PROTOTYPE SHOWCASE</title>
    <link href=""https://fonts.googleapis.com/css2?family=Outfit:wght@300;400;600;700&display=swap"" rel=""stylesheet"">
    <style>
        :root { --primary-color: #6366f1; --bg-dark: #0f172a; }
        body { margin: 0; padding: 0; font-family: 'Outfit', sans-serif; background-color: var(--bg-dark); background-image: radial-gradient(at 0% 0%, rgba(99, 102, 241, 0.15) 0px, transparent 50%), radial-gradient(at 100% 0%, rgba(236, 72, 153, 0.15) 0px, transparent 50%), radial-gradient(at 100% 100%, rgba(20, 184, 166, 0.15) 0px, transparent 50%), radial-gradient(at 0% 100%, rgba(245, 158, 11, 0.1) 0px, transparent 50%); background-attachment: fixed; min-height: 100vh; display: flex; justify-content: center; align-items: flex-start; }
        .container { width: 90%; max-width: 1200px; margin-top: 50px; }
        .dashboard-card { background: rgba(255, 255, 255, 0.05); backdrop-filter: blur(20px); border: 1px solid rgba(255, 255, 255, 0.1); border-radius: 20px; padding: 30px; box-shadow: 0 8px 32px 0 rgba(0, 0, 0, 0.3); color: #fff; }
        .header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 30px; }
        .filters { display: flex; gap: 20px; margin-bottom: 30px; }
        .input-glass { background: rgba(255, 255, 255, 0.05); border: 1px solid rgba(255, 255, 255, 0.2); border-radius: 10px; padding: 12px 20px; color: #fff; flex: 1; outline: none; }
        .btn { padding: 10px 24px; border-radius: 12px; border: none; cursor: pointer; font-weight: 600; transition: all 0.3s ease; }
        .btn-primary { background: var(--primary-color); color: white; box-shadow: 0 4px 15px rgba(99, 102, 241, 0.4); }
        .btn-primary:hover { transform: translateY(-2px); box-shadow: 0 6px 20px rgba(99, 102, 241, 0.6); }
        .glass-table { width: 100%; border-collapse: collapse; text-align: left; }
        .glass-table th, .glass-table td { padding: 15px; border-bottom: 1px solid rgba(255, 255, 255, 0.1); }
        .glass-table th { font-weight: 600; text-transform: uppercase; font-size: 0.8rem; color: rgba(255, 255, 255, 0.6); }
        .badge { padding: 6px 14px; border-radius: 20px; font-size: 0.75rem; font-weight: 700; text-transform: uppercase; display: inline-block; }
        .badge-red { background: rgba(255, 77, 77, 0.2); color: #ff4d4d; border: 1px solid #ff4d4d; box-shadow: 0 0 10px rgba(255, 77, 77, 0.3); }
        .badge-green { background: rgba(46, 204, 113, 0.2); color: #2ecc71; border: 1px solid #2ecc71; box-shadow: 0 0 10px rgba(46, 204, 113, 0.3); }
        .badge-orange { background: rgba(243, 156, 18, 0.2); color: #f39c12; border: 1px solid #f39c12; box-shadow: 0 0 10px rgba(243, 156, 18, 0.3); }
        .badge-gray { background: rgba(149, 165, 166, 0.2); color: #95a5a6; border: 1px solid #95a5a6; box-shadow: 0 0 10px rgba(149, 165, 166, 0.3); }
        h1 { font-weight: 300; letter-spacing: 2px; }
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""dashboard-card"">
            <div class=""header"">
                <h1>BUG MANAGEMENT DASHBOARD</h1>
                <button class=""btn btn-primary"">+ Create Bug</button>
            </div>
            <div class=""filters"">
                <input type=""text"" placeholder=""Search bugs..."" class=""input-glass"">
                <select class=""input-glass"" style=""max-width: 200px;"">
                    <option>All Statuses</option>
                    <option>Open</option>
                    <option>WIP</option>
                    <option>Closed</option>
                </select>
            </div>
            <table class=""glass-table"">
                <thead>
                    <tr>
                        <th>ID</th><th>Title</th><th>Description</th><th>Status</th><th>Created At</th><th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr><td>101</td><td>Login button unresponsive</td><td>React state not updating on click</td><td><span class=""badge badge-red"">Open</span></td><td>2026-04-01</td><td>✏️ 🗑️</td></tr>
                    <tr><td>102</td><td>API connection timeout</td><td>Intermittent 503 errors on search</td><td><span class=""badge badge-orange"">WIP</span></td><td>2026-03-28</td><td>✏️ 🗑️</td></tr>
                    <tr><td>103</td><td>Navbar alignment issue</td><td>Shifted by 5px on Safari</td><td><span class=""badge badge-gray"">Hold</span></td><td>2026-03-25</td><td>✏️ 🗑️</td></tr>
                    <tr><td>104</td><td>CSS loading error</td><td>Missing fonts on first load</td><td><span class=""badge badge-green"">Closed</span></td><td>2026-03-20</td><td>✏️ 🗑️</td></tr>
                </tbody>
            </table>
        </div>
    </div>
</body>
</html>", "text/html"));

app.MapFallbackToFile("index.html");

app.Run();
