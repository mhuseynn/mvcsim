using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Contexts;
using WebApplication8.Models.Concretes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(p =>
{
    p.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});


builder.Services.AddIdentity<AppUser, IdentityRole>(p =>
{
    p.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
