using ApplicationBlog.DBContext;
using ApplicationBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountController_Test
{
    public class MockBlogRepository : IBlogRepository
    {        
        public MockBlogRepository() 
        { 
            
        }
        public Users Login(Login objRequest)
        {
            return new Users() {Username = "",Password="" };
        }
        public Users Register_AlreadyAvailable(Users objUsers)
        {
            return new Users() { Username = "", Password = "" };
        }
        public int Register(Users objUsers)
        {            
            return 1;
        }
        public int Register_UpdateProfilePic(long UserId, string FilePath)
        {
            return 1;
        }
        public List<Country> GetCountry()
        {
            return new List<Country>() { new Country() {CountryId= 1, Countryname="US", IsActive=true} };
        }
        public List<State> GetState()
        {
            return new List<State>() { new State() { StateId = 1, Statename = "Maharashtra", IsActive = true } };
        }
        public List<City> GetCity(long id)
        {
            return new List<City>() { new City() { CityId = 1, StateId = 1, Cityname = "Mumbai", IsActive = true } };
        }

        public List<UserPostList> GetAllPost(long UserId)
        {
            return new List<UserPostList>() { new UserPostList() {
                UserId = 1,
                UserPostId = 1,
                PostType = "text",
                PostText = "Test",
                PostMediaPath = "",
                LikeCount = 1,
                CommentCount = 1,
                PostedOn = DateTime.Now,
                Firstname = "Test",
                Lastname = "Test",
                ProfilePic = ""
            } };
        }
        public List<UserPostList> GetPersonalPost(long? UserId)
        {
            return new List<UserPostList>() { new UserPostList() {
                UserId = 1,
                UserPostId = 1,
                PostType = "text",
                PostText = "Test",
                PostMediaPath = "",
                LikeCount = 1,
                CommentCount = 1,
                PostedOn = DateTime.Now,
                Firstname = "Test",
                Lastname = "Test",
                ProfilePic = ""
            } };
        }
        public int SubmitPost(UserPost objPost)
        {
            return 1;
        }
        public GetProfileDetails GetProfileDetails(long UserId)
        {            
            return new GetProfileDetails()
            {
                UserId = 1,
                Username = "",
                Firstname = "",
                Lastname = "",
                Mobile = "",
                Gender = 'M',
                DOB = DateTime.Now,
                ProfilePic = "",
                BackgroundPic = "",
                ProfileStatus = "",
                RegisteredOn = DateTime.Now,
                Countryname = ""
            };
        }
        public List<Users> SearchUsers(string SearchText)
        {
            return new List<Users>() { new Users() { Username = "", Password = "" } };
        }
        public List<GetPostLikeList> SubmitPostLike(long UserId, long UserPostId)
        {
            return new List<GetPostLikeList>() { new GetPostLikeList() {UserId=1, FirstName = "", LastName = "", ProfilePic = "" } };
        }
        public List<GetPostCommentsList> GetPostComments(long UserId, long UserPostId)
        {
            return new List<GetPostCommentsList>() 
            { 
                new GetPostCommentsList() { UserId = 1, FirstName = "", LastName = "", ProfilePic = "" } 
            };
        }
        public int SubmitPostComment(UserPostComment objUserPostComment)
        {
            return 1;
        }
        public int IncreaseUserPostCommentCount(long UserPostId)
        {
            return 1;
        }
        public int DeletePost(long UserId, long UserPostId)
        {
            return 1;
        }
        public UserPostComment DeleteComment(long UserId, long UserPostCommentId)
        {
            return new UserPostComment() { UserPostCommentId = 1, UserId = 1, UserPostId = 1, CommentText = "" };
        }
        public int DecreaseUserPostCommentCount(long? UserPostId)
        {
            return 1;
        }
        public int UpdateProfileStatus(long UserId, string ProfileStatus)
        {
            return 1;
        }
    }
}
