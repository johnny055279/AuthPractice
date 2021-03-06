using System.Security.Claims;
using Basic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("BasicAuth").AddCookie("BasicAuth", config => {
    config.LoginPath = "/Home/Authenticate";
    config.Cookie.Name = "BaseAuth.com";
});

builder.Services.AddAuthorization(config => {

    config.AddPolicy("CustomClaim", provider => {

        provider.AddRequirements(new CustomClaimRequirement(ClaimTypes.DateOfBirth));
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endPoints =>
{
    endPoints.MapDefaultControllerRoute();
});

app.Run();

