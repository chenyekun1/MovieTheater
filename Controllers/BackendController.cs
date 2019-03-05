using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieTheater.Controllers
{
    public sealed class BackendController : Controller
    {
        private readonly WebContext _context;

        public
        BackendController(WebContext context) => _context = context;

        public IActionResult
        Index() => View();

        public IActionResult
        Create()
        {
            ViewBag.movie_category_id = new SelectList(_context.MovieCategories, "CategoryId", "CategoryName");
            ViewBag.movie_country_id  = new SelectList(_context.MovieCountries, "CountryId", "CountryName");
            return View();
        }

        public async Task<IActionResult>
        MovieManageIndex(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"]  = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GradeSortParm"] = sortOrder == "Grade" ? "grade_desc" : "grade";
            ViewData["CurrentFilter"] = searchString;

            var movies = _context.Movies
                                 .Include(m => m.movie_category)
                                 .Include(m => m.movie_country)
                                 .AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.MovieName.Contains(searchString) ||
                                           m.MovieDescription.Contains(searchString) ||
                                           m.MovieDirector.Contains(searchString) ||
                                           m.MovieActor.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    movies = movies.OrderByDescending(m => m.MovieName);
                    break;
                case "grade":
                    movies = movies.OrderBy(m => m.MovieGrade);
                    break;
                case "grade_desc":
                    movies = movies.OrderByDescending(m => m.MovieGrade);
                    break;
                default:
                    movies = movies.OrderBy(m => m.MovieName);
                    break;
            }

            return View(await movies.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult>
        MovieEdit(int? id)
        {
            if (id == null)    return NotFound();
            
            var movie = await _context.Movies.Include(m => m.movie_category)
                                             .Include(m => m.movie_country)
                                      .SingleOrDefaultAsync(m => m.MovieId == id);

            if (movie == null) return NotFound();
            return View(movie);
        }

        public async Task<IActionResult>
        MovieDetails(int? id)
        {
            if (id == null)    return NotFound();
            
            var movie = await _context.Movies.Include(m => m.movie_category)
                                             .Include(m => m.movie_country)
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(m => m.MovieId == id);

            if (movie == null) return NotFound();
            return View(movie);
        }
    
        public async Task<IActionResult>
        MovieDelete(int? id, bool? saveChangesError = false)
        {
            if (id == null)    return NotFound();

            var movie = await _context.Movies
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(m => m.MovieId == id);

            if (movie == null) return NotFound();

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again, and if the problem persists " +
                "see your system administrator.";
            }
            throw new NotImplementedException();
        }


        public IActionResult
        ALogin() => View();

        public async Task<IActionResult>
        ALogin(string uid, string pwd)
        {
            var user = await _context.Admins
                                     .FirstOrDefaultAsync(a => a.AdminName.Equals(uid) &&
                                                               a.AdminPwd.Equals(pwd));
            
            // Log in fault
            if (user == null) return View();

            HttpContext.Session.SetString("user_group", "admin");
            return RedirectToAction("Index", "Backend");
        }
    }
}