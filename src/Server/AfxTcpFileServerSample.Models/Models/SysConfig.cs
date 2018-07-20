using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Models
{
    [Table("SysConfig")]
    public class SysConfig : IModel
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Type")]
        [Index("IX_SysConfig_Type")]
        public ConfigType Type { get; set; }

        [Required]
        [Column("Key")]
        [MaxLength(100)]
        public string Key { get; set; }

        [Required]
        [Column("Value")]
        [MaxLength(4 * 1024)]
        public string Value { get; set; }
    }
}
