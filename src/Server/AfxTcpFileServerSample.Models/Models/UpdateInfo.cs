using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Models
{
    [Table("UpdateInfo")]
    public class UpdateInfo : IModel, IUpdateTime
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Type")]
        public UpdateInfoType Type { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Version")]
        public string Version { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("FilePath")]
        public string FilePath { get; set; }

        [Required]
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
