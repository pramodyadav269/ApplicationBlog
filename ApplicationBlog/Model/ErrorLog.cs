using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class ErrorLog
    {
        [Key]
        public long ErrorId { get; set; }

        public long? UserId { get; set; }

        [Column(TypeName ="varchar(50)")]
        public string ControllerName { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string ActionName { get; set; }

        public DateTime? RequestTime { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string JSONRequest { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string ErrorStackTrace { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string ClientIP { get; set; }
    }
}
