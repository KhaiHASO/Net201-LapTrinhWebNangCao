using Demo03.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm các dịch vụ vào container.
builder.Services.AddControllersWithViews();

// 2. Cấu hình DbContext với Lazy Loading Proxies (Slide 18 + 20)
// Phải cài đặt: Microsoft.EntityFrameworkCore.Proxies
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies() // <--- QUAN TRỌNG CHO LAZY LOADING
           .UseSqlServer(builder.Configuration.GetConnectionString("NET201Slide8Demo03")));

var app = builder.Build();

// 3. Cấu hình đường ống xử lý HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 4. Định tuyến mặc định (Customer/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}");

// 5. LOGIC TỰ ĐỘNG MIGRATION & SEEDING (Chạy tự động khi khởi động)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Tương đương với 'update-database'
        Console.WriteLine("Database Migrated Successfully!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();
