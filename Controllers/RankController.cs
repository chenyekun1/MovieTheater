using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Models;

namespace MovieTheater.Controllers
{
    public class RankController : Controller
    {
        private readonly WebContext _context;
        public RankController(WebContext context) => _context = context;

        public async Task<IActionResult>
        Index()
        {
            var movies = await _context.Movies
                                       .AsNoTracking()
                                       .OrderByDescending(m => m.MovieGrade)
                                       .ToListAsync();
            movies.Sort(new MovieGradeComparer());
            var top_movies = new List<Movie>();
            
            for (int i=0; i<10; i++)
                top_movies.Add(movies[i]);
                
            return View(top_movies);
        }
    }
}

//Todo
