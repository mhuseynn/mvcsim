using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Models.Concretes;

namespace WebApplication8.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public DbSet<Chef> Chefs { get; set; }
}
