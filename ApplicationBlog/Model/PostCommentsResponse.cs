namespace ApplicationBlog.Model
{
    public class PostCommentsResponse : Response
    {
        public List<GetPostCommentsList> lstDetails { get; set; }
    }
    public class GetPostCommentsList
    {
        public long? UserPostCommentId { get; set; }
        public long? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
        public string CommentText { get; set; }
        public bool IsCurrentUser { get; set; }        
    }
}
