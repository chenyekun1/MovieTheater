using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("movie")]
    internal sealed class Movie
    {
        [Column("id")]
        [Key]
        public int MovieId { get; set; }

        [Column("movie_name")]
        [MaxLength(50)]
        public string MovieName { get; set; }

        [Column("movie_director")]
        [MaxLength(20)]
        public string MovieDirector { get; set; }

        [Column("movie_grade")]
        public string MovieGrade { get; set; }

        [Column("movie_actor")]
        [MaxLength(20)]
        public string MovieActor { get; set; }

        [Column("movie_description")]
        [MaxLength(500)]
        public string MovieDescription { get; set; }

        [Column("movie_price")]
        public int MoviePrice { get; set; }

        [Column("movie_category_id")]
        public Nullable<int> MovieCategoryId { get; set; }

        [Column("movie_country_id")]
        public Nullable<int> MovieCountryId { get; set; }

        [Column("movie_img")]
        public string MovieImg { get; set; }

        public MovieCategory movie_category { get; set; }
        public MovieCountry movie_country { get; set; }
    }
}