using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebApplication8.Areas.Admin.ViewModels;
using WebApplication8.Contexts;
using WebApplication8.Models.Concretes;

namespace WebApplication8.Areas.Admin.Controllers;
[Area("Admin")]
public class ChefController : Controller
{
    AppDbContext _context;
    IWebHostEnvironment _environment;

    public ChefController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Chef> blogs = await _context.Chefs.ToListAsync();
        return View(blogs);
    }



    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ChefVM chefVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (chefVM.formFile.Length > 2500000)
        {
            ModelState.AddModelError("fileError", "File Is Big");
            return View();
        }

        var safeFileName = Path.GetFileName(chefVM.formFile.FileName);
        string fileName;

        if (safeFileName.Length > 100)
        {
            fileName = Guid.NewGuid().ToString() + safeFileName.Substring(safeFileName.Length - 64);
        }
        else
        {
            fileName = Guid.NewGuid().ToString() + safeFileName;
        }

        string path = Path.Combine(_environment.WebRootPath.ToString() + "\\images\\", fileName);
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            chefVM.formFile.CopyTo(stream);
        }


        chefVM.ImgUrl = fileName;

        Chef chef = new Chef()
        {
            FullName = chefVM.FullName,
            Designation = chefVM.Designation,
            ImgUrl = chefVM.ImgUrl,
        };

        await _context.Chefs.AddAsync(chef);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Update(int id)
    {
        Chef a = await _context.Chefs.FirstOrDefaultAsync(b => b.Id == id);


        ChefVM chefVM = new ChefVM()
        {
            Id = id,
            FullName = a.FullName,
            Designation = a.Designation,
            ImgUrl = a.ImgUrl,

        };

        return View(chefVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ChefVM chefVM)
    {
        if (chefVM.ImgUrl == null)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("blog", "Sehvdir");
                return View();
            }
            if (chefVM.formFile.Length > 2500000)
            {
                ModelState.AddModelError("fileError", "File Is Big");
                return View();
            }

            var safeFileName = Path.GetFileName(chefVM.formFile.FileName);
            string fileName;

            if (safeFileName.Length > 100)
            {
                fileName = Guid.NewGuid().ToString() + safeFileName.Substring(safeFileName.Length - 64);
            }
            else
            {
                fileName = Guid.NewGuid().ToString() + safeFileName;
            }

            string path = Path.Combine(_environment.WebRootPath.ToString() + "\\images\\", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                chefVM.formFile.CopyTo(stream);
            }
            chefVM.ImgUrl = fileName;

            Chef a = _context.Chefs.FirstOrDefault(x => x.Id == chefVM.Id);

            a.FullName = chefVM.FullName;
            a.Designation = chefVM.Designation;
            a.ImgUrl = chefVM.ImgUrl;


            _context.Chefs.Update(a);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        else
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("blog", "Sehvdir");
                return View();
            }
            Chef a = _context.Chefs.FirstOrDefault(x => x.Id == chefVM.Id);
            
            a.FullName = chefVM.FullName;
            a.Designation = chefVM.Designation;
            a.ImgUrl = chefVM.ImgUrl;
            _context.Chefs.Update(a);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }


    public async Task<IActionResult> Delete(int id)
    {
        Chef a = await _context.Chefs.FirstOrDefaultAsync(b => b.Id == id);
        _context.Chefs.Remove(a!);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }





}
