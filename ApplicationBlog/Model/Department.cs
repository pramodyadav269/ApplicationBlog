using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class Department
    {
        [Key]
        public long DepartmentId { get; set; }
        public string DeptName { get; set; }
        public Employee ObjEmployee {get; set;}        
    }
}
