using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfxTcpFileServerSample.Models
{
    [Table("User")]
    public class User : IModel, IUpdateTime, ICreateTime, IIsDelete
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("RoleId")]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Account")]
        [Index("IX_User_Account")]
        public string Account { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Password")]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [Column("IsSystem")]
        public bool IsSystem { get; set; }

        [Required]
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }

        [Required]
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

        [Required]
        [Column("IsDelete")]
        public bool IsDelete { get; set; }

    }
}
