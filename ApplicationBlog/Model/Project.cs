using System.ComponentModel.DataAnnotations;

namespace ApplicationBlog.Model
{
    public class Project
    {
        [Key]
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public ICollection<EmployeeProjectMapping> LstEmployeeProjectMapping { get; set; }
    }
}
