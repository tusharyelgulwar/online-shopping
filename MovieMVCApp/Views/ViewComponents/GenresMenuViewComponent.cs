using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieMVCApp.ViewComponents
{
    public class GenresMenuViewComponent : ViewComponent
    {
        private readonly IGenreService _genreService;

        public GenresMenuViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return View(genres);
        }
    }
}