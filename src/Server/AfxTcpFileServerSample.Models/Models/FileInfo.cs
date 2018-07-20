using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Models
{
    [Table("FileInfo")]
    public class FileInfo : IModel, IUpdateTime, ICreateTime, IIsDelete
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("ParentId")]
        public int ParentId { get; set; }

        [Required]
        [Column("Type")]
        public FileInfoType Type { get; set; }

        [Required]
        [Column("Directory")]
        [MaxLength(255)]
        [Index("IX_FileInfo_Directory")]
        public string Directory { get; set; }

        [Required]
        [Column("Name")]
        [MaxLength(255)]
        [Index("IX_FileInfo_Name")]
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
        [Column("Key")]
        [MaxLength(50)]
        public string Key { get; set; }

        [Required]
        [Column("CheckStatus")]
        public CheckStatusType CheckStatus { get; set; }

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
