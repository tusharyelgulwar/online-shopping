using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Contracts.Services;

namespace MovieMVCApp.Controllers
{
    [Authorize] // Require login for all actions
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // View Profile / Account
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Account()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                // Not logged in — redirect to login
                return RedirectToAction("Login", "Account");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("Invalid user ID");
            }

            var profile = await _userService.GetUserProfileAsync(userId);

            if (profile == null)
            {
                return NotFound("User profile not found.");
            }

            return View(profile);
        }


        //purchases
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var userId = int.Parse(userIdClaim.Value);
            var movies = await _userService.GetPurchasedMoviesAsync(userId);

            return View(movies);
        }


        // Favorites
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(userIdClaim.Value);

            var movies = await _userService.GetFavoriteMoviesAsync(userId);

            return View(movies);
        }

    }
}