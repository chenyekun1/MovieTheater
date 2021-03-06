using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Models;

namespace MovieTheater.Controllers
{
    public sealed class BackendController : Controller
    {
        private readonly WebContext _context;

        public
        BackendController(WebContext context) => _context = context;

        public IActionResult
        Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("user_group")))
                return RedirectToAction("ALogin", "UserGate");
            if (HttpContext.Session.GetString("user_group").Equals("admin"))
                return View();
            else
                return RedirectToAction("ALogin", "UserGate");
        }

        public IActionResult
        Create()
        {
            ViewBag.movie_category_id = _context.MovieCategories;
            ViewBag.movie_country_id  = _context.MovieCountries;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
        Create(int id,
              [Bind("MovieId,MovieName," +
                    "MovieActor," +
                    "MovieDescription," +
                    "MovieDirector," +
                    "MovieGrade," +
                    "MovieImg," +
                    "MoviePrice," +
                    "MovieCategoryId," +
                    "MovieCountryId")]
            Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.AddAsync(movie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(MovieManageIndex));
                }
            } catch (Exception)
            {
                Console.WriteLine("Update Error.");
                throw;
            }
            return View(movie);
        }
        
        public async Task<IActionResult>
        MovieManageIndex(string sortOrder, string searchString)
        {

            if (HttpContext.Session.GetString("user_group") != "admin")
                return RedirectToAction(nameof(Index));

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
            ViewBag.movieCategory = _context.MovieCategories;
            ViewBag.movieCountry  = _context.MovieCountries;
            
            if (movie == null) return NotFound();
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
        MovieEdit(int id,
                  [Bind("MovieId,MovieName," +
                        "MovieActor," +
                        "MovieDescription," +
                        "MovieDirector," +
                        "MovieGrade," +
                        "MovieImg," +
                        "MoviePrice," +
                        "MovieCategoryId," +
                        "MovieCountryId")]
                  Movie movie)
        {
            if (id != movie.MovieId) return NotFound();
            if (!ModelState.IsValid) return View(movie);
            
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            } catch (Exception)
            {
                if (!_context.Movies.Any(m => m.MovieId == movie.MovieId)) return NotFound();
                else                                                       throw;
            }

            return RedirectToAction(nameof(MovieManageIndex));
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

            return View(movie);
        }
    
        public async Task<IActionResult>
        DeleteConfirm(int id)
        {
            var movie = await _context.Movies.AsNoTracking()
                                             .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null) return RedirectToAction(nameof(MovieManageIndex));

            try
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MovieManageIndex));
            } catch (DbUpdateException)
            {
                return RedirectToAction(nameof(MovieDelete), new {id = id});
            }
        }
    
        public async Task<IActionResult>
        CategoryManageIndex()
        {
            if (!HttpContext.Session.GetString("user_group").Equals("admin"))
                return RedirectToAction("Index", "Home");
            return View(await _context.MovieCategories
                                      .AsNoTracking()
                                      .ToListAsync());
        }

        public IActionResult
        CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
        CreateCategory(int id,
                       [Bind("CategoryId,CategoryName")] MovieCategory category)
        {
            if (id != category.CategoryId)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.AddAsync(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(CategoryManageIndex));
                } catch (Exception)
                {
                    Console.WriteLine("Category Create Error.");
                    throw;
                }
            }
            return View(category);
        }

        public async Task<IActionResult>
        CountryManageIndex()
        {
            if (!HttpContext.Session.GetString("user_group").Equals("admin"))
                return RedirectToAction("Index", "Home");
            return View(await _context.MovieCountries
                                      .AsNoTracking()
                                      .ToListAsync());
        }
    
        public IActionResult
        CreateCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
        CreateCountry(int id,
                      [Bind("CountryId,CountryName")] MovieCountry country)
        {
            if (id != country.CountryId)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.AddAsync(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(CountryManageIndex));
                } catch (Exception)
                {
                    Console.WriteLine("Create Country error.");
                    throw;
                }
            }

            return View(country);
        }
    }
}