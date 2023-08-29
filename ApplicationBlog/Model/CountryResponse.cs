using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class CountryResponse : Response
    {
        //public List<Country> lstDetails { get; set; }
        public List<CountryList> lstDetails { get; set; }
    }
    public class CountryList
    {
        public long CountryId { get; set; }

        public string Countryname { get; set; }
    }
}
