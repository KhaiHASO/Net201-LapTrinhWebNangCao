using Microsoft.EntityFrameworkCore;
using Demo03.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Demo03.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // NOTE: We do not call context.Database.EnsureCreated() or Migrate() here automatically per request.
        // But we try to Seed if table exists. 
        // Ideally, DbInitializer checks context.Products.Any(), which throws if table missing.
        // So we wrap in try-catch to allow first-run without tables (user runs migration manually).
        
        // However, user Requirement says: "Gọi Seeder này trong Program.cs nhưng chỉ khi database đã được tạo"
        // Validating DB connection and existence is safer.
        if (context.Database.CanConnect())
        {
             DbInitializer.Initialize(context);
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
