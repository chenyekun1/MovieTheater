using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("admin")]
    public sealed class Admin
    {
        [Key]
        [Column("admin_id")]
        public int AdminId {get;set;}

        [Column("admin_name")]
        [MaxLength(20)]
        public string AdminName{get;set;}
        
        [Column("admin_pwd")]
        [MaxLength(20)]
        public string AdminPwd {get;set;}
    }
}