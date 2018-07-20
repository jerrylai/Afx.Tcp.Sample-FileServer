using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Models
{
    [Table("RoleAuth")]
    public class RoleAuth : IModel
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("RoleId")]
        public int RoleId { get; set; }

        [Required]
        [Column("Type")]
        public AuthType Type { get; set; }
    }
}
