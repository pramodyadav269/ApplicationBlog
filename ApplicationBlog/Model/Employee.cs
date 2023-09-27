using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class Employee
    {
        [Key]
        public long EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        
        [ForeignKey("Department")]
        public long DepartmentId { get; set; }
        public Department ObjDepartment { get; set; }


        public ICollection<AppModule> LstAppModule { get; set; }
        public ICollection<EmployeeProjectMapping> LstEmployeeProjectMapping { get; set; }
    }
}
