using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("movie_country")]
    internal sealed class MovieCountry
    {
        [Column("country_id")]
        [Key]
        public int CountryId { get; set; }

        [Column("country_name")]
        [MaxLength(50)]
        public string CountryName { get; set; }

        public ICollection<Movie> movies { get; set; }
    }
}