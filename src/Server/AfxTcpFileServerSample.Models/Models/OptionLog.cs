using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfxTcpFileServerSample.Models
{
    [Table("OptionLog")]
    public class OptionLog:IModel, ICreateTime
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Type")]
        public int Type { get; set; }

        [Required]
        [Column("UserId")]
        public int UserId { get; set; }

        [Required]
        [Column("UserAccount")]
        [MaxLength(100)]
        public string UserAccount { get; set; }

        [Required]
        [Column("UserName")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [Column("Address")]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [Column("Msg")]
        [MaxLength(1024)]
        public string Msg { get; set; }

        [Required]
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }
    }
}
