﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebSiteBanHang.Repositories;
using WebSiteBanHang.Interfaces;
using Microsoft.AspNetCore.Identity;
using WebSiteBanHang.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thay đổi cấu hình DbContext để sử dụng Firebird
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseFirebird(builder.Configuration.GetConnectionString("FirebirdConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
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
app.UseAuthentication();
app.UseAuthorization();

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