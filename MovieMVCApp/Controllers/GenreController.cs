using ApplicationCore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MovieMVCApp.Controllers;

public class GenreController : Controller
{
    private readonly IGenreRepository _genreRepository;
    
    public GenreController(IGenreRepository genreRepository)
    {
        this._genreRepository = genreRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        var data = _genreRepository.GetAll();
        return View(data);
    }
}