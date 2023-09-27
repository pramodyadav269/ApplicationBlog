using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class AppModule
    {
        [Key]
        public long AppModuleId { get; set; }
        public string AppModuleName { get; set; }

        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public Employee ObjEmployee { get; set; }

    }
}
