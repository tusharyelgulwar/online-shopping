using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieMVCApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> TopMovies(DateTime? fromDate, DateTime? toDate)
    {
        var movies = await _adminService.GetTopMoviesAsync(fromDate, toDate);
        return View(movies);
    }


    [HttpGet]
    public IActionResult CreateMovie()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(Movie model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _adminService.CreateMovieAsync(model);
        return RedirectToAction("TopMovies");
    }
}
