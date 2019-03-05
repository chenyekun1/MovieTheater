using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("movie_country")]
    public sealed class MovieCountry
    {
        [Column("country_id")]
        [Key]
        [Display(Name = "地区ID")]
        public int CountryId { get; set; }

        [Column("country_name")]
        [MaxLength(50)]
        [Display(Name = "地区")]
        public string CountryName { get; set; }

        public ICollection<Movie> movies { get; set; }
    }
}