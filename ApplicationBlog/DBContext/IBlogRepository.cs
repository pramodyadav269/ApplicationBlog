using ApplicationBlog.Model;

namespace ApplicationBlog.DBContext
{
    public interface IBlogRepository
    {
        //List<Employee> GetEmpDetails();

        Users Login(Login objRequest);
        Users Register_AlreadyAvailable(Users objUsers);
        int Register(Users objUsers);
        int Register_UpdateProfilePic(long UserId, string FilePath);
        List<Country> GetCountry();
        List<State> GetState();
        List<City> GetCity(long id);

        List<UserPostList> GetAllPost(long UserId);
        List<UserPostList> GetPersonalPost(long? UserId);
        int SubmitPost(UserPost objPost);
        GetProfileDetails GetProfileDetails(long UserId);
        List<Users> SearchUsers(string SearchText);
        List<GetPostLikeList> SubmitPostLike(long UserId, long UserPostId);
        List<GetPostCommentsList> GetPostComments(long UserId, long UserPostId);
        int SubmitPostComment(UserPostComment objUserPostComment);
        int IncreaseUserPostCommentCount(long UserPostId);
        int DeletePost(long UserId, long UserPostId);
        UserPostComment DeleteComment(long UserId, long UserPostCommentId);
        int DecreaseUserPostCommentCount(long? UserPostId);
        int UpdateProfileStatus(long UserId, string ProfileStatus);
    }
}
