﻿using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }



}
