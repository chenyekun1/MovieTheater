using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("comment")]
    public sealed class Comment
    {
        [Key]
        [Column("commentId")]
        [Display(Name = "评论Id")]
        public int CommentId { get; set; }

        [Column("customer_id")]
        [Display(Name = "评论者Id")]
        public int CustomerId { get; set; }

        [Column("movie_id")]
        [Display(Name = "电影Id")]
        public int MovieId { get; set; }

        [Column("comment_context")]
        [Display(Name = "评论内容")]
        [MaxLength(300)]
        public string CommentContext { get; set; }

        [Column("comment_date")]
        [Display(Name = "评论时间")]
        public DateTime CommentDate { get; set; }

        public Movie Movie { get; set; }
        public Customer Customer { get; set; }
    }
}