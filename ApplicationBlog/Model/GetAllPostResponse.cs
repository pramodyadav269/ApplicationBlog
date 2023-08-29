namespace ApplicationBlog.Model
{
    public class GetAllPostResponse : Response
    {
        public List<UserPostList> lstDetails { get; set; }
    }
    public class UserPostList
    {
        public long? UserId { get; set; }
        public long? UserPostId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ProfilePic { get; set; }
        public string PostType { get; set; }
        public string PostText { get; set; }
        public string PostMediaPath { get; set; }
        public long? LikeCount { get; set; }
        public long? CommentCount { get; set; }
        public DateTime? PostedOn { get; set; }
        public bool IsCurrentUser { get; set; }
        public List<GetPostCommentsList> lstComments { get; set; }
    }
}
