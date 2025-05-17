using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication8.Contexts;
using WebApplication8.Models.Concretes;

namespace WebApplication8.Controllers;


[Authorize(Roles ="Admin,Member")]
public class HomeController : Controller
{
    AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Chef> chefList =await _context.Chefs.ToListAsync();
        return View(chefList);
    }
}
