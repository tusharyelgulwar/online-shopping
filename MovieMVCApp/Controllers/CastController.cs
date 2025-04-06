using Microsoft.AspNetCore.Mvc;

namespace MovieMVCApp.Controllers;

public class CastController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}