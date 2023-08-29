using System.ComponentModel.DataAnnotations;

namespace ApplicationBlog.Model
{
    public class SubmitPost
    {
        [Required]
        public string PostType { get; set; }
        
        public string PostText { get; set; }
       
        public string PostMedia { get; set; }

    }
}
