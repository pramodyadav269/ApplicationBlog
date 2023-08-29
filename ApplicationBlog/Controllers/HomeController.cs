using ApplicationBlog.DBContext;
using ApplicationBlog.Migrations;
using ApplicationBlog.Model;
using ApplicationBlog.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using static ApplicationBlog.Utility.ValidationMessages;

namespace ApplicationBlog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IConfiguration _config;
        private readonly BlogDbContext _blogDB;
        private IBlogRepository _blogRepo;

        public HomeController(IConfiguration config, BlogDbContext blogDB)
        {
            _config = config;
            _blogDB = blogDB;
            _blogRepo = new BlogRepository(_config, _blogDB);
        }

        [NonAction]
        private Users GetClaims()
        {
            Users objUser = null;
            var s = HttpContext.Request.Headers["Authorization"];
            if (AuthenticationHeaderValue.TryParse(s, out var headerValue))
            {
                // we have a valid AuthenticationHeaderValue that has the following details:
                var scheme = headerValue.Scheme;
                var parameter = headerValue.Parameter;

                // scheme will be "Bearer"
                // parmameter will be the token itself.
                // or
                var stream = parameter;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

                objUser = new Users()
                {
                    UserId = Convert.ToInt64(tokenS.Claims.FirstOrDefault(a => a.Type == "UserId")?.Value),
                    Username = tokenS.Claims.FirstOrDefault(a => a.Type == "Username")?.Value,
                    Firstname = tokenS.Claims.FirstOrDefault(a => a.Type == "Firstname")?.Value,
                    Lastname = tokenS.Claims.FirstOrDefault(a => a.Type == "Lastname")?.Value,
                    Mobile = tokenS.Claims.FirstOrDefault(a => a.Type == "Mobile")?.Value
                };                
            }
            //var claims =(new AuthenticeToken(_config).ValidateToken(HttpContext.Request.Headers["Authorization"].ToString()));
            return objUser;
        }
        
        [HttpGet]
        [Route("GetAllPost")]
        public IActionResult GetAllPost(long? id)
        {
            IActionResult response = null;
            List<UserPostList> lstUserPost = new List<UserPostList>();

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                lstUserPost = _blogRepo.GetAllPost(objUsers.UserId);

                if (lstUserPost != null && lstUserPost.Count > 0)
                {
                    response = Ok(new GetAllPostResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                        lstDetails = lstUserPost
                    });
                }
                else
                {
                    response = Ok(new GetAllPostResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.NoRecordFound,
                        lstDetails = null
                    });
                }
            }
            else
            {
                response = Ok(new GetAllPostResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg,
                    lstDetails = null
                });
            }
            return response;
        }

        [HttpGet]
        [Route("GetPersonalPost")]
        public IActionResult GetPersonalPost(long? id)
        {
            IActionResult response = null;
            List<UserPostList> lstUserPost = new List<UserPostList>();

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                lstUserPost = _blogRepo.GetPersonalPost(id);

                if (lstUserPost != null && lstUserPost.Count > 0)
                {
                    response = Ok(new GetAllPostResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                        lstDetails = lstUserPost
                    });
                }
                else
                {
                    response = Ok(new GetAllPostResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.NoRecordFound,
                        lstDetails = null
                    });
                }
            }
            else
            {
                response = Ok(new GetAllPostResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg,
                    lstDetails = null
                });
            }
            return response;
        }

        [HttpPost]
        [Route("SubmitPost")]
        public IActionResult SubmitPost(SubmitPost objRequest)
        {
            IActionResult response = null;

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                if (ModelState.IsValid)
                {
                    if(objRequest.PostType.ToLower() == ValidationMessages.PostType.Image || objRequest.PostType.ToLower() == ValidationMessages.PostType.Video)
                    {
                        var BasePath = _config["AppBasePath"].ToString();

                        string RelativeFolderPath = objRequest.PostType.ToLower() == ValidationMessages.PostType.Image ? "image\\" : "video\\";
                        RelativeFolderPath = "\\Files\\MediaFiles\\" + RelativeFolderPath;
                        objRequest.PostMedia = (new CommonUtility()).SaveMediaFile(objUsers.UserId, objRequest.PostMedia, BasePath, RelativeFolderPath);
                    }                    

                    UserPost objPost = new UserPost()
                    {
                        UserId = objUsers.UserId,
                        PostedOn = DateTime.Now,
                        PostType = objRequest.PostType,
                        PostText = objRequest.PostText,
                        PostMediaPath = objRequest.PostMedia,
                        LikeCount = 0,
                        CommentCount = 0,
                        IsActive = true
                    };

                    _blogRepo.SubmitPost(objPost);

                    response = Ok(new SubmitPostResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.SubmitUserPost.PostSubmitted
                    });
                }
                else
                {
                    response = Ok(new SubmitPostResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.ErrorCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                        Description = ValidationMessages.HttpRequestCode.AllMandatoryParameter
                    });
                }
            }
            else
            {
                response = Ok(new SubmitPostResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg
                });
            }
            return response;
        }
        
        [HttpGet]
        [Route("GetProfileDetails")]
        public IActionResult GetProfileDetails()
        {
            IActionResult response = null;

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                GetProfileDetails objUser = _blogRepo.GetProfileDetails(objUsers.UserId);
                if (objUser != null)
                {
                    GetProfileDetailsResponse objResponse = new GetProfileDetailsResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                        UserId = objUser.UserId,
                        Username = objUser.Username,
                        Firstname = objUser.Firstname,
                        Lastname = objUser.Lastname,
                        Mobile = objUser.Mobile,
                        Gender = objUser.Gender,
                        DOB = objUser.DOB,
                        ProfilePicPath = objUser.ProfilePic,
                        BackgroundPic = objUser.BackgroundPic,
                        ProfileStatus = objUser.ProfileStatus,
                        RegisteredOn = objUser.RegisteredOn.HasValue ? objUser.RegisteredOn.Value.ToString("yyyy-MM-dd") : "",
                        Countryname = objUser.Countryname
                    };
                    response = Ok(objResponse);
                }
                else
                {
                    response = Ok(new GetProfileDetailsResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.NoRecordFound
                    });
                }
            }
            else
            {
                response = Ok(new SubmitPostResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg
                });
            }
            return response;
        }

        [HttpGet]
        [Route("SearchUsers")]
        public IActionResult SearchUsers(string SearchText)
        {
            IActionResult response = null;

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                List<Users> lstUser = _blogRepo.SearchUsers(SearchText);
                if (lstUser != null)
                {
                    SearchUsersResponse objResponse = new SearchUsersResponse();
                    List<GetProfileDetailsResponse> lstProfiles = new List<GetProfileDetailsResponse>();
                    foreach (Users profile in lstUser)
                    {
                        GetProfileDetailsResponse objProfile = new GetProfileDetailsResponse()
                        {
                            UserId = profile.UserId,
                            Username = profile.Username,
                            Firstname = profile.Firstname,
                            Lastname = profile.Lastname,
                            Mobile = profile.Mobile,
                            Gender = profile.Gender,
                            DOB = profile.DOB,
                            ProfilePicPath = profile.ProfilePic
                        };
                        lstProfiles.Add(objProfile);
                    }
                    objResponse.StatusCode = ValidationMessages.HttpRequestCode.SuccessCode;
                    objResponse.StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg;
                    objResponse.Description = ValidationMessages.HttpRequestCode.SuccessDescription;
                    objResponse.lstDetails = lstProfiles;

                    response = Ok(objResponse);
                }
                else
                {
                    response = Ok(new SearchUsersResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.NoRecordFound
                    });
                }
            }
            return response;
        }

        [HttpGet]
        [Route("SubmitPostLike/{id}")]
        public IActionResult SubmitPostLike(long id)
        {
            IActionResult response = null;

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                List<GetPostLikeList> lstPostLike = _blogRepo.SubmitPostLike(objUsers.UserId, id);

                response = Ok(new PostLikeResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                    Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                    lstDetails = lstPostLike
                });
            }
            else
            {
                response = Ok(new PostLikeResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg,
                    lstDetails = null
                });
            }
            return response;
        }

        [HttpGet]
        [Route("GetPostComments/{id}")]
        public IActionResult GetPostComments(long id)
        {
            IActionResult response = null;

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                List<GetPostCommentsList> lstPostComments = _blogRepo.GetPostComments(objUsers.UserId,id);
                if (lstPostComments != null)
                {
                    response = Ok(new PostCommentsResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                        lstDetails = lstPostComments
                    });
                }
                else
                {
                    response = Ok(new PostCommentsResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.NoRecordFound
                    });
                }
            }
            else
            {
                response = Ok(new PostCommentsResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg
                });
            }
            return response;
        }

        [HttpPost]
        [Route("SubmitPostComment")]
        public IActionResult SubmitPostComment(SubmitPostComment objRequest)
        {
            IActionResult response = null;
            if (ModelState.IsValid)
            {
                Users objUsers = GetClaims();
                if (objUsers != null)
                {
                    UserPostComment objUserPostComment = new UserPostComment()
                    {
                        UserId = objUsers.UserId,
                        UserPostId = objRequest.UserPostId,
                        CommentText = objRequest.CommentText,
                        CommentedOn = DateTime.Now,
                        IsActive = true
                    };

                    _blogRepo.SubmitPostComment(objUserPostComment);
                    _blogRepo.IncreaseUserPostCommentCount(objRequest.UserPostId);

                    List<GetPostCommentsList> lstPostComments = _blogRepo.GetPostComments(objUsers.UserId, objRequest.UserPostId);
                    if (lstPostComments != null)
                    {
                        response = Ok(new PostCommentsResponse()
                        {
                            StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                            StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                            Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                            lstDetails = lstPostComments
                        });
                    }
                    else
                    {
                        response = Ok(new PostCommentsResponse()
                        {
                            StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                            StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                            Description = ValidationMessages.HttpRequestCode.NoRecordFound
                        });
                    }
                }
                else
                {
                    response = Ok(new PostCommentsResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                        Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg
                    });
                }
            }
            else
            {
                response = Ok(new PostCommentsResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.ErrorCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.AllMandatoryParameter
                });
            }
            return response;
        }

        [HttpGet]
        [Route("DeletePost/{id}")]
        public IActionResult DeletePost(long id)
        {
            IActionResult response = null;

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                var postRecord = _blogRepo.DeletePost(objUsers.UserId, id);
                if (postRecord != null)
                {                    
                    response = Ok(new Response()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.SuccessDescription
                    });
                }
                else
                {
                    response = Ok(new Response()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.NoRecordFound
                    });
                }
            }
            else
            {
                response = Ok(new PostCommentsResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg
                });
            }
            return response;
        }

        [HttpGet]
        [Route("DeleteComment/{id}")]
        public IActionResult DeleteComment(long id)
        {
            IActionResult response = null;

            Users objUsers = GetClaims();
            if (objUsers != null)
            {
                var postCommentRecord = _blogRepo.DeleteComment(objUsers.UserId, id);
                if (postCommentRecord != null)
                {
                    var postRecord = _blogRepo.DecreaseUserPostCommentCount(postCommentRecord.UserPostId);

                    List<GetPostCommentsList> lstPostComments = 
                        _blogRepo.GetPostComments(objUsers.UserId, Convert.ToInt64(postCommentRecord.UserPostId));

                    response = Ok(new PostCommentsResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                        lstDetails = lstPostComments
                    });
                }
                else
                {
                    response = Ok(new PostCommentsResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.NoRecordFound
                    });
                }
            }
            else
            {
                response = Ok(new PostCommentsResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg
                });
            }
            return response;
        }

        [HttpPost]
        [Route("UpdateProfileStatus")]
        public IActionResult UpdateProfileStatus(UpdateProfileStatus objRequest)
        {
            IActionResult response = null;
            if (ModelState.IsValid)
            {
                Users objUsers = GetClaims();
                if (objUsers != null)
                {
                    _blogRepo.UpdateProfileStatus(objUsers.UserId, objRequest.ProfileStatus);

                    response = Ok(new Response()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.HttpRequestCode.SuccessDescription
                    });
                }
                else
                {
                    response = Ok(new Response()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.UnauthorizedCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                        Description = ValidationMessages.HttpRequestCode.UnauthorizedMsg
                    });
                }
            }
            else
            {
                response = Ok(new Response()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.ErrorCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.AllMandatoryParameter
                });
            }
            return response;
        }
    }
}
