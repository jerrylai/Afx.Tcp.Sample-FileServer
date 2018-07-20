using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfxTcpFileServerSample.Models
{
    [Table("ServerInfo")]
    public class ServerInfo : IModel, IUpdateTime, ICreateTime , IIsDelete
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Port")]
        public int Port { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Address")]
        [Index("IX_ServerInfo_Address")]
        public string Address { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Account")]
        public string Account { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Password")]
        public string Password { get; set; }

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
