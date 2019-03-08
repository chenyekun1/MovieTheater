using MovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MovieTheater.Models;

namespace MovieTheater.ViewModel
{
    public class MovieDetailsViewModel
    {
        private readonly WebContext _context;

        public MovieDetailsViewModel(WebContext context, Customer customer, Movie movie)
        {
            _context = context;
            Customer = customer;
            Movie = movie;
        }

        public async void
        LoadComments()
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == Movie.MovieId);
            Comments = movie.Comments;
        }

        public ICollection<Comment> Comments { get; private set; }
        public Customer Customer { get; }
        public Movie Movie { get; }
    }
}