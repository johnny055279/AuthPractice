using Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(config => {

    config.UseInMemoryDatabase("InMemoryDatabase");

});

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Home/Index";
    config.Cookie.Name = "IdentityCookie";
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(config => {

    config.Password.RequireDigit = false;
    config.Password.RequiredLength = 4;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();

