using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MovieTheater.Controllers
{
    public sealed class MovieController : Controller
    {
        private readonly WebContext _context;

        public
        MovieController(WebContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult>
        Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"]  = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GradeSortParm"] = sortOrder == "Grade" ? "grade_desc" : "grade";
            ViewData["CurrentFilter"] = searchString;

            var movies = from s in _context.Movies
                         select s;

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
        Details(int? id)
        {
            if (id == null)    return NotFound();
            
            var movie = await _context.Movies.Include(m => m.movie_category)
                                             .Include(m => m.movie_country)
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(m => m.MovieId == id);

            if (movie == null) return NotFound();
            return View(movie);
        }
    }
}