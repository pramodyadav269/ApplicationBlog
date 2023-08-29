namespace ApplicationBlog.Model
{
    public class PostLikeResponse : Response
    {
        public List<GetPostLikeList> lstDetails { get; set; }
    }
    public class GetPostLikeList
    {
        public long? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
    }
}
