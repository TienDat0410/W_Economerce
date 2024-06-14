using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebSiteBanHang.Repositories;
using WebSiteBanHang.Interfaces;
using Microsoft.AspNetCore.Identity;
using WebSiteBanHang.Models;
using WebSiteBanHang.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cấu hình DbContext để sử dụng Firebird
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseFirebird(builder.Configuration.GetConnectionString("FirebirdConnection")));

// Cấu hình Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Đăng ký các repository
builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

// Thêm cấu hình cho IWebHostEnvironment
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);
// Cấu hình session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Sử dụng session
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Định tuyến cho các controller
app.MapControllerRoute(
    name: "products",
    pattern: "/products",
    defaults: new { controller = "Products", action = "Index" });

app.MapControllerRoute(
    name: "categories",
    pattern: "/categories",
    defaults: new { controller = "Categories", action = "Index" });

app.MapControllerRoute(
    name: "orders",
    pattern: "/orders",
    defaults: new { controller = "Orders", action = "Index" });

app.MapControllerRoute(
    name: "orderItems",
    pattern: "/orderItems",
    defaults: new { controller = "OrderItems", action = "Index" });

app.MapControllerRoute(
    name: "addresses",
    pattern: "/addresses",
    defaults: new { controller = "Addresses", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();