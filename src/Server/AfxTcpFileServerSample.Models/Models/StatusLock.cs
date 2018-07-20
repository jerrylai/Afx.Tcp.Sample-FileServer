using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Models
{
    [Table("StatusLock")]
    public class StatusLock : IModel, IUpdateTime
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Type")]
        public StatusLockType Type { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Key")]
        public string Key { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Column("Owner")]
        public string Owner { get; set; }

        [Required]
        [Column("IsLock")]
        public bool IsLock { get; set; }

        [Required]
        [Column("Timeout")]
        public DateTime Timeout { get; set; }

        [Required]
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
