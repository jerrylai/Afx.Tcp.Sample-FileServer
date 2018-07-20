using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Models
{
    [Table("Role")]
    public class Role : IModel, IUpdateTime, ICreateTime, IIsDelete
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Type")]
        public RoleType Type { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Name")]
        [Index("IX_Role_Name")]
        public string Name { get; set; }

        [Required]
        [Column("IsSystem")]
        public bool IsSystem { get; set; }

        [Required]
        [Column("Key")]
        [MaxLength(50)]
        public string Key { get; set; }

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
