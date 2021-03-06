using MovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MovieTheater.Models;
using System.Linq;
using System;

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
            var movie = await _context.Movies
                                      .Include(m => m.Comments)
                                        .ThenInclude(c => c.Customer)
                                      .FirstOrDefaultAsync(m => m.MovieId == Movie.MovieId);
            var comments = movie.Comments.ToList();

            comments.Sort((x, y) => DateTime.Compare(x.CommentDate, y.CommentDate));
            comments.Reverse();
            Comments = comments;
        }

        public ICollection<Comment> Comments { get; private set; }
        public Customer Customer { get; }
        public Movie Movie { get; }
    }
}