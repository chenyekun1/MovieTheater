using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using MovieTheater.ViewModel;
using MovieTheater.Models;

namespace MovieTheater.Controllers
{
    public sealed class MovieController : Controller
    {
        private readonly WebContext _context;

        public
        MovieController(WebContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult>
        Index(string sortOrder, string searchCategoryString, string searchCountryString)
        {
            ViewData["NameSortParm"]  = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GradeSortParm"] = sortOrder == "Grade" ? "grade_desc" : "grade";
            ViewData["CurrentCategoryFilter"] = searchCategoryString;
            ViewData["CurrentCountryFilter"]  = searchCountryString;

            var movies = _context.Movies
                                 .Include(m => m.movie_category)
                                 .Include(m => m.movie_country)
                                 .AsNoTracking();
            
            ViewBag.movieCategory = await _context.MovieCategories.ToListAsync();
            ViewBag.movieCountry  = await _context.MovieCountries.ToListAsync();

            if (!string.IsNullOrEmpty(searchCategoryString))
            {
                movies = movies.Where(m => m.movie_category.CategoryName
                                                           .Equals(searchCategoryString));
            }
            if (!string.IsNullOrEmpty(searchCountryString))
            {
                movies = movies.Where(m => m.movie_country.CountryName
                                                          .Equals(searchCountryString));
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

            return View(await movies.ToListAsync());
        }

        public async Task<IActionResult>
        Details(int? id)
        {
            if (id == null)    return NotFound();
            
            var movie = await _context.Movies.Include(m => m.movie_category)
                                             .Include(m => m.movie_country)
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(m => m.MovieId == id);

            var customer = await _context.Customers
                                         .SingleOrDefaultAsync(c => 
                                                               c.CustomerId == HttpContext.Session
                                                                                          .GetInt32("user_id"));
            
            var movieDetailsViewModel = new MovieDetailsViewModel(_context, customer, movie);
            movieDetailsViewModel.LoadComments();

            if (movie == null) return NotFound();
            return View(movieDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult>
        PublicComment(string context, int movieId)
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            if (userId == null)
                return RedirectToAction("Details", "Movie", new {id = movieId});

            var comment = new Comment{CustomerId = userId.Value,
                                      CommentDate = DateTime.Now,
                                      CommentContext = context,
                                      MovieId = movieId};

            _context.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Movie", new {id = movieId});
        }
    }
}