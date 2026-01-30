using Microsoft.EntityFrameworkCore;
using NET201Slide8Demo02.Data;

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào container.
builder.Services.AddControllersWithViews();

// 1. Đăng ký DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NET201Slide8Demo02")));

var app = builder.Build();

// Cấu hình đường ống xử lý HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Giá trị HSTS mặc định là 30 ngày. Bạn có thể muốn thay đổi điều này cho các kịch bản production, xem https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 2. Cấu hình định tuyến mặc định đến Student Controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

// 3. Tự động Migration & Seed Data
// Tự động chạy Migration khi ứng dụng khởi động
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Áp dụng mọi pending migrations vào database. 
        // Nếu DB chưa có, nó sẽ tự tạo.
        context.Database.Migrate(); 
        Console.WriteLine("Database Migrated Successfully.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();
