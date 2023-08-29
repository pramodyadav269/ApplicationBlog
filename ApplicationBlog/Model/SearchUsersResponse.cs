namespace ApplicationBlog.Model
{
    public class SearchUsersResponse :Response
    {
        public List<GetProfileDetailsResponse> lstDetails = new List<GetProfileDetailsResponse>();
    }
}
