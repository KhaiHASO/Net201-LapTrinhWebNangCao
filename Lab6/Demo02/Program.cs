using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Config DB Context
builder.Services.AddDbContext<Demo02.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Config DI Services (LifeCycle Demo)
// Demo 1: Basic DI (Calculator) - Thay đổi class ở đây để demo
//builder.Services.AddTransient<Demo02.Services.Calculators.ICalculatorService, Demo02.Services.Calculators.StandardCalculator>();
builder.Services.AddTransient<Demo02.Services.Calculators.ICalculatorService, Demo02.Services.Calculators.BlackFridayCalculator>();

// Demo 2: Life Cycle
builder.Services.AddTransient<Demo02.Services.ITransientService, Demo02.Services.TransientService>();
builder.Services.AddScoped<Demo02.Services.IScopedService, Demo02.Services.ScopedService>();
builder.Services.AddSingleton<Demo02.Services.ISingletonService, Demo02.Services.SingletonService>();

var app = builder.Build();

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
