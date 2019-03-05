using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("movie_category")]
    public sealed class MovieCategory
    {
        [Key]
        [Column("category_id")]
        [Display(Name = "类别ID")]
        public int CategoryId { get; set; }

        [Column("category_name")]
        [MaxLength(50)]
        [Display(Name = "类别")]
        public string CategoryName { get; set; }

        public ICollection<Movie> movies { get; set; }
    }
}