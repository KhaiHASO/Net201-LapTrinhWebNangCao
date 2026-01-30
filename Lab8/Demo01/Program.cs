using Demo01.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm các dịch vụ (Services)
builder.Services.AddControllersWithViews();

// 2. Cấu hình DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NET201Slide8Demo01")));

var app = builder.Build();

// 3. Cấu hình Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 4. Định tuyến mặc định (Order/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Index}/{id?}");

// 5. LOGIC TỰ ĐỘNG MIGRATION (Quan trọng!)
// Tự động tạo database và seed data khi chạy app
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Lệnh này tương đương 'update-database'
        Console.WriteLine("Database Migrated Successfully!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();
