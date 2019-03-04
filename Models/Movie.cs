using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("movie")]
    internal sealed class Movie
    {
        [Column("movie_id")]
        [Key]
        internal int MovieId { get; set; }

        [Column("movie_name")]
        [MaxLength(50)]
        internal string MovieName { get; set; }

        [Column("movie_director")]
        [MaxLength(20)]
        internal string MovieDirector { get; set; }

        [Column("movie_grade")]
        internal string MovieGrade { get; set; }

        [Column("movie_actor")]
        [MaxLength(20)]
        internal string MovieActor { get; set; }

        [Column("movie_description")]
        [MaxLength(500)]
        internal string MovieDescription { get; set; }

        [Column("movie_price")]
        internal int MoviePrice { get; set; }

        [Column("movie_category_id")]
        internal Nullable<int> MovieCategoryId { get; set; }

        [Column("movie_country_id")]
        internal Nullable<int> MovieCountryId { get; set; }

        [Column("movie_img")]
        internal string MovieImg { get; set; }

        internal MovieCategory movie_category { get; set; }
        internal MovieCountry movie_country { get; set; }
    }
}