using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfxTcpFileServerSample.Models
{
    [Table("ReadPathInfo")]
    public class ReadPathInfo : IModel
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Path")]
        [MaxLength(255)]
        [Index("IX_ReadPathInfo_Path")]
        public string Path { get; set; }
    }
}
