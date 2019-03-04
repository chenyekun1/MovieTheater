using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("movie_category")]
    internal sealed class MovieCategory
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("category_name")]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        public ICollection<Movie> movies { get; set; }
    }
}