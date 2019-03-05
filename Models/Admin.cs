using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("admin")]
    public sealed class Admin
    {
        [Key]
        [Column("admin_id")]
        [Display(Name = "管理员ID")]
        public int AdminId {get;set;}

        [Column("admin_name")]
        [MaxLength(20)]
        [Display(Name = "管理员名称")]
        public string AdminName{get;set;}
        
        [Column("admin_pwd")]
        [MaxLength(20)]
        [Display(Name = "管理员密码")]
        public string AdminPwd {get;set;}
    }
}