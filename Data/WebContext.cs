using MovieTheater.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieTheater.Data
{
    public sealed class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<MovieCountry> MovieCountries { get; set; }
    }
}