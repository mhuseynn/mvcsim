using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication8.Models.Concretes;
using WebApplication8.ViewModels;

namespace WebApplication8.Controllers;

public class AccountController : Controller
{
    UserManager<AppUser> _userManager;
    RoleManager<IdentityRole> _roleManager;
    SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {



        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("register", "Register Error");
            return View(registerVM);
        }

        AppUser user = new AppUser()
        {
            Name = registerVM.Name,
            Surname=registerVM.Surname,
            UserName = registerVM.Username,
            Email = registerVM.Email,
        };


        var result = await _userManager.CreateAsync(user,registerVM.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Login");
        }

        return View(registerVM);
    }

    public IActionResult Login()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }

        AppUser user = await _userManager.FindByEmailAsync(loginVM.Email);

        if (user == null)
        {

            return View(loginVM);
        }


        var check = await _userManager.CheckPasswordAsync(user, loginVM.Password);

        if(!check)
        {
            return View(loginVM);
        }

        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);


        if (result.Succeeded)
        {
            if (returnUrl != null)
                return RedirectPermanent(returnUrl!);
            return RedirectToAction("Index", "Home");
        }

        return View(loginVM);
    }



    public async Task<IActionResult> CreateRoles()
    {
        await _roleManager.CreateAsync(new IdentityRole("Admin"));
        await _roleManager.CreateAsync(new IdentityRole("Member"));

        return RedirectToAction("Register");
    }


}
