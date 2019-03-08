using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models
{
    [Table("customer")]
    public sealed class Customer
    {
        [Key]
        [Column("customerId")]
        [Display(Name = "客户ID")]
        public int CustomerId { get; set; }

        [Column("customer_name")]
        [Display(Name = "客户名称")]
        [MaxLength(20)]
        public string CustomerName { get; set; }

        [Column("customer_pwd")]
        [Display(Name = "客户密码")]
        [MaxLength(20)]
        public string CustomerPwd { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}