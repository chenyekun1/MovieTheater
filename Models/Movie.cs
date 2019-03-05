using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("movie")]
    public sealed class Movie
    {
        [Column("id")]
        [Key]
        [Display(Name = "电影ID")]
        public int MovieId { get; set; }

        [Column("movie_name")]
        [MaxLength(50)]
        [Display(Name = "电影名称")]
        public string MovieName { get; set; }

        [Column("movie_director")]
        [MaxLength(20)]
        [Display(Name = "电影导演")]
        public string MovieDirector { get; set; }

        [Column("movie_grade")]
        [Display(Name = "评分")]
        public string MovieGrade { get; set; }

        [Column("movie_actor")]
        [MaxLength(20)]
        [Display(Name = "主要演员")]
        public string MovieActor { get; set; }

        [Column("movie_description")]
        [MaxLength(500)]
        [Display(Name = "电影信息")]
        public string MovieDescription { get; set; }

        [Column("movie_price")]
        [Display(Name = "票价")]
        public int MoviePrice { get; set; }

        [Column("movie_category_id")]
        [Display(Name = "电影分类ID")]
        public Nullable<int> MovieCategoryId { get; set; }

        [Column("movie_country_id")]
        [Display(Name = "电影地区ID")]
        public Nullable<int> MovieCountryId { get; set; }

        [Column("movie_img")]
        public string MovieImg { get; set; }

        public MovieCategory movie_category { get; set; }
        public MovieCountry movie_country { get; set; }
    }
}