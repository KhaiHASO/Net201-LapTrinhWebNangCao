using Demo03.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Services to the container.
builder.Services.AddControllersWithViews();

// 2. Configure DbContext with Lazy Loading Proxies (Slide 18 + 20)
// Must install: Microsoft.EntityFrameworkCore.Proxies
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies() // <--- CRITICAL FOR LAZY LOADING
           .UseSqlServer(builder.Configuration.GetConnectionString("NET201Slide8Demo03")));

var app = builder.Build();

// 3. Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 4. Default Route (Customer/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}");

// 5. AUTO MIGRATION & SEEDING (Run automatically on startup)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Equivalent to 'update-database'
        Console.WriteLine("Database Migrated Successfully!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();
