using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Text;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Models
{
    [Table("SyncInfo")]
    public class SyncInfo : IModel
    {
        [Key]
        [Column("ServerId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServerId { get; set; }

        [Required]
        [Column("Type")]
        public SyncType Type { get; set; }

        [Column("SyncId")]
        public int? SyncId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("SyncKey")]
        public string SyncKey { get; set; }

        [Column("SyncUpdateTime")]
        public DateTime? SyncUpdateTime { get; set; }
    }
}
