using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class StateResponse : Response
    {
        public List<State> lstDetails = new List<State>();
    }
    
}
