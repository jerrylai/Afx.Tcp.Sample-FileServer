using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfxTcpFileServerSample.Models
{
    [Table("TempFile")]
    public class TempFile : IModel, ICreateTime, IUpdateTime
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Directory")]
        [MaxLength(255)]
        [Index("IX_TempFile_Directory")]
        public string Directory { get; set; }

        [Required]
        [Column("Name")]
        [MaxLength(255)]
        [Index("IX_TempFile_Name")]
        public string Name { get; set; }

        [Required]
        [Column("Length")]
        public long Length { get; set; }

        [Required]
        [Column("CreationTime")]
        public DateTime CreationTime { get; set; }

        [Required]
        [Column("LastWriteTime")]
        public DateTime LastWriteTime { get; set; }

        [Required]
        [Column("TempIndex")]
        public long TempIndex { get; set; }

        [Required]
        [Column("TempName")]
        [MaxLength(255)]
        public string TempName { get; set; }

        [Required]
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }

        [Required]
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }
    }
}
