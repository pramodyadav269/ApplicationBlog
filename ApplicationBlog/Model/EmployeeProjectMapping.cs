using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class EmployeeProjectMapping
    {
        [Key]
        public long MappingId { get; set; }
        
        
        [ForeignKey("Project")]
        public long ProjectId { get; set; }
        public Project ObjProject { get; set; }


        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public Employee ObjEmployee { get; set; }
    }
}
