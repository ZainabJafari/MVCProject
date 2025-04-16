using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVCProject.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToPage("/Account/Register", new { area = "Identity" });
    }
}
