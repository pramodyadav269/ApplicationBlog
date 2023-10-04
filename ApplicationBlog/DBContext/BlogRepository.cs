using ApplicationBlog.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security;

namespace ApplicationBlog.DBContext
{
    public class BlogRepository : IBlogRepository
    {
        private IConfiguration _config;
        private readonly BlogDbContext _blogDB;

        public BlogRepository(IConfiguration config, BlogDbContext blogDB) 
        {
            _config = config;
            _blogDB = blogDB;
        }


        public List<Employee> GetEmpDetails()//Example of Eager Loading,Lazy Loading & Explicit Loading
        {
            // Eager Loading
            var employeesWithAppModules = _blogDB.tblEmployee.Include(e => e.LstAppModule).ToList();
            foreach (var employee in employeesWithAppModules)
            {
                foreach (var module in employee.LstAppModule)
                {
                    var test = module.AppModuleName;
                }
            }

            // Lazy Loading
            var lazyLoadedEmployee = _blogDB.tblEmployee.First();
            foreach (var module in lazyLoadedEmployee.LstAppModule)
            {
                var test = module.AppModuleName;
            }
             
            // Explicit Loading
            var explicitLoadedEmployee = _blogDB.tblEmployee.First();
            _blogDB.Entry(explicitLoadedEmployee).Collection(e => e.LstAppModule).Load();            
            foreach (var module in explicitLoadedEmployee.LstAppModule)
            {
                var test = module.AppModuleName;
            }

            return employeesWithAppModules;
        }


        public Users Login(Login objRequest)
        {
            return _blogDB.tblUsersMaster.Where(
                    x => x.Username.Equals(objRequest.Username) && x.Password.Equals(objRequest.Password) 
                            && x.IsActive == true).FirstOrDefault();
        }
        public Users Register_AlreadyAvailable(Users objUsers)
        {
            return _blogDB.tblUsersMaster.FirstOrDefault(
                    x => (x.Username == objUsers.Username || x.Mobile == objUsers.Mobile) && x.IsActive == true);
        }
        public int Register(Users objUsers)
        {
            _blogDB.tblUsersMaster.Add(objUsers);
            return _blogDB.SaveChanges();
        }
        public int Register_UpdateProfilePic(long UserId, string FilePath)
        {
            var userRecord = _blogDB.tblUsersMaster.FirstOrDefault(x => x.UserId == UserId && x.IsActive == true);
            userRecord.ProfilePic = FilePath;
            return _blogDB.SaveChanges();
        }
        public List<Country> GetCountry()
        {
            return _blogDB.tblCountryMaster.ToList();
        }
        public List<State> GetState()
        {
            return _blogDB.tblStateMaster.ToList();
        }        
        public List<City> GetCity(long id)
        {
            return _blogDB.tblCityMaster.Where(x => x.StateId.Equals(id)).ToList();
        }
        
        public List<UserPostList> GetAllPost(long UserId)
        {
            List<UserPostList> lstUserPost = new List<UserPostList>();
            var query = from t1 in _blogDB.tblUserPost
                        join t2 in _blogDB.tblUsersMaster on t1.UserId equals t2.UserId
                        where t1.IsActive == true
                        select new
                        {
                            UserId = t1.UserId,
                            UserPostId = t1.UserPostId,
                            PostType = t1.PostType,
                            PostText = t1.PostText,
                            PostMediaPath = t1.PostMediaPath,
                            LikeCount = t1.LikeCount,
                            CommentCount = t1.CommentCount,
                            PostedOn = t1.PostedOn,
                            Firstname = t2.Firstname,
                            Lastname = t2.Lastname,
                            ProfilePic = t2.ProfilePic,
                            IsCurrentUser = t2.UserId == UserId ? true : false,
                        };

            var result = query.OrderByDescending(result => result.PostedOn).ToList();
            foreach (var item in result)
            {
                UserPostList objUserPost = new UserPostList()
                {
                    UserId = item.UserId,
                    UserPostId = item.UserPostId,
                    PostType = item.PostType,
                    PostText = item.PostText,
                    PostMediaPath = item.PostMediaPath,
                    LikeCount = item.LikeCount,
                    CommentCount = item.CommentCount,
                    PostedOn = item.PostedOn,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    ProfilePic = item.ProfilePic,
                    IsCurrentUser = item.IsCurrentUser,
                    lstComments = (new List<GetPostCommentsList>())
                };
                lstUserPost.Add(objUserPost);
            }
            return lstUserPost;
        }
        public List<UserPostList> GetPersonalPost(long? UserId)
        {
            List<UserPostList> lstUserPost = new List<UserPostList>();
            var query = from t1 in _blogDB.tblUserPost
                        join t2 in _blogDB.tblUsersMaster on t1.UserId equals t2.UserId
                        where t1.IsActive == true && t1.UserId == UserId
                        select new
                        {
                            UserId = t1.UserId,
                            UserPostId = t1.UserPostId,
                            PostType = t1.PostType,
                            PostText = t1.PostText,
                            PostMediaPath = t1.PostMediaPath,
                            LikeCount = t1.LikeCount,
                            CommentCount = t1.CommentCount,
                            PostedOn = t1.PostedOn,
                            Firstname = t2.Firstname,
                            Lastname = t2.Lastname,
                            ProfilePic = t2.ProfilePic
                        };

            var result = query.OrderByDescending(result => result.PostedOn).ToList();
            foreach (var item in result)
            {
                UserPostList objUserPost = new UserPostList()
                {
                    UserId = item.UserId,
                    UserPostId = item.UserPostId,
                    PostType = item.PostType,
                    PostText = item.PostText,
                    PostMediaPath = item.PostMediaPath,
                    LikeCount = item.LikeCount,
                    CommentCount = item.CommentCount,
                    PostedOn = item.PostedOn,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    ProfilePic = item.ProfilePic,
                    IsCurrentUser = true,
                    lstComments = (new List<GetPostCommentsList>())
                };
                lstUserPost.Add(objUserPost);
            }
            return lstUserPost;
        }
        public int SubmitPost(UserPost objPost)
        {
            _blogDB.tblUserPost.Add(objPost);
            return _blogDB.SaveChanges();
        }
        public GetProfileDetails GetProfileDetails(long UserId)
        {
            var query = from t1 in _blogDB.tblUsersMaster
                        join t2 in _blogDB.tblCountryMaster on t1.CountryId equals t2.CountryId
                        where t1.UserId == UserId && t1.IsActive == true
                        select new GetProfileDetails
                        {
                            UserId = t1.UserId,
                            Username = t1.Username,
                            Firstname = t1.Firstname,
                            Lastname = t1.Lastname,
                            Mobile = t1.Mobile,
                            Gender = t1.Gender,
                            DOB = t1.DOB,
                            ProfilePic = t1.ProfilePic,
                            BackgroundPic = t1.BackgroundPic,
                            ProfileStatus = t1.ProfileStatus,
                            RegisteredOn = t1.RegisteredOn,
                            Countryname = t2.Countryname
                        };

            return query.FirstOrDefault();
        }
        public List<Users> SearchUsers(string SearchText)
        {
            IQueryable<Users> lstUser = _blogDB.tblUsersMaster.Where(x =>
                                                                (x.Username.Contains(SearchText) ||
                                                                x.Firstname.Contains(SearchText) ||
                                                                x.Lastname.Contains(SearchText) ||
                                                                x.Mobile.Contains(SearchText)) && x.IsActive == true
                                                            );
            return lstUser.ToList();
        }
        public List<GetPostLikeList> SubmitPostLike(long UserId, long UserPostId)
        {
            return (new DBUtility(_config, _blogDB)).SubmitPostLike(UserId, UserPostId);
        }
        public List<GetPostCommentsList> GetPostComments(long UserId, long UserPostId)
        {
            return (new DBUtility(_config, _blogDB)).GetPostComments(UserPostId, UserId);
        }
        public int SubmitPostComment(UserPostComment objUserPostComment)
        {
            _blogDB.tblUserPostComment.Add(objUserPostComment);
            return _blogDB.SaveChanges();
        }
        public int IncreaseUserPostCommentCount(long UserPostId)
        {
            var postRecord = _blogDB.tblUserPost.FirstOrDefault(x => x.UserPostId == UserPostId && x.IsActive == true);
            postRecord.CommentCount = postRecord.CommentCount + 1;
            return _blogDB.SaveChanges();
        }
        public int DeletePost(long UserId, long UserPostId)
        {
            var postRecord = _blogDB.tblUserPost.FirstOrDefault(x => x.UserPostId == UserPostId && x.UserId == UserId && x.IsActive == true);
            postRecord.IsActive = false;
            return _blogDB.SaveChanges();
        }
        public UserPostComment DeleteComment(long UserId, long UserPostCommentId)
        {
            var postCommentRecord = _blogDB.tblUserPostComment.FirstOrDefault(x => x.UserPostCommentId == UserPostCommentId 
                                                                            && x.UserId == UserId && x.IsActive == true);
            postCommentRecord.IsActive = false;
            _blogDB.SaveChanges();
            return postCommentRecord;
        }
        public int DecreaseUserPostCommentCount(long? UserPostId)
        {
            var postRecord = _blogDB.tblUserPost.FirstOrDefault(x => x.UserPostId == UserPostId && x.IsActive == true);
            postRecord.CommentCount = postRecord.CommentCount - 1;
            return _blogDB.SaveChanges();
        }
        public int UpdateProfileStatus(long UserId, string ProfileStatus)
        {
            var postUser = _blogDB.tblUsersMaster.FirstOrDefault(x => x.UserId == UserId && x.IsActive == true);
            postUser.ProfileStatus = ProfileStatus;
            return _blogDB.SaveChanges();
        }
    }
}
