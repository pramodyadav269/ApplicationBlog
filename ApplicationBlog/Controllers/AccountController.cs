using ApplicationBlog.DBContext;
using ApplicationBlog.Model;
using ApplicationBlog.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace ApplicationBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private readonly BlogDbContext _blogDB;
        private IBlogRepository _blogRepo;

        public AccountController(IConfiguration config, BlogDbContext blogDB)
        {
            _config = config;
            _blogDB = blogDB;
            _blogRepo = new BlogRepository(config, _blogDB);
        }
        
        [NonAction]
        private string GenerateToken(Users objUser)
        {
            // generate token that is valid for 7 days
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            //var claims = new List<Claim>()
            //{
            //    new Claim("UserId", objUser.UserId.ToString()),
            //    new Claim("Username", objUser.Username),
            //    new Claim("Firstname", objUser.Firstname),
            //    new Claim("Lastname", objUser.Lastname),
            //    new Claim("Mobile", objUser.Mobile.ToString()),
            //    new Claim("ProfilePicPath", objUser.ProfilePicPath.ToString()),
            //};

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new[] { new Claim("Username", objUser.Username) }),
            //    //Claims = claims,
            //    Expires = DateTime.UtcNow.AddSeconds(30),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("UserId", objUser.UserId.ToString()),
                new Claim("Username", objUser.Username),
                new Claim("Firstname", objUser.Firstname),
                new Claim("Lastname", objUser.Lastname),
                new Claim("Mobile", objUser.Mobile.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login objRequest)
        {
            IActionResult response = Unauthorized();
            if (ModelState.IsValid)
            {
                //_blogRepo.GetEmpDetails();//Example of Eager Loading,Lazy Loading & Explicit Loading

                objRequest.Password = HashPassword.GetHashPassword(objRequest.Password);
                var _user = _blogRepo.Login(objRequest);

                if (_user != null)
                {
                    var token = GenerateToken(_user);                    
                    response = Ok(new LoginResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                        Description = ValidationMessages.Login.SuccessLogin,
                        Token = token,
                        ProfilePic = _user.ProfilePic
                    }); ;
                }
                else
                {
                    response = Ok(new LoginResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.ErrorCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                        Description = ValidationMessages.Login.InvalidCredentials
                    });
                }
            }
            else
            {
                response = Ok(new LoginResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.ErrorCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                    Description = ValidationMessages.HttpRequestCode.AllMandatoryParameter
                });
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(Register objRequest)
        {
            IActionResult response = null;
            //try 
            //{
                if (ModelState.IsValid)
                {
                    Users objUsers = new Users()
                    {
                        Username = objRequest.Username,
                        Password = HashPassword.GetHashPassword(objRequest.Password),
                        Firstname = objRequest.Firstname,
                        Lastname = objRequest.Lastname,
                        Mobile = objRequest.Mobile,
                        Gender = objRequest.Gender,
                        DOB = DateTime.Parse(objRequest.DOB),
                        ProfilePic = string.Empty,
                        BackgroundPic = string.Empty,
                        ProfileStatus = ValidationMessages.Register.ProfileDefaultStatus,
                        CountryId = Convert.ToInt32(objRequest.CountryId),
                        RegisteredOn = DateTime.Now,
                        IsActive = true
                    };

                    var userAlreadyAvailable = _blogRepo.Register_AlreadyAvailable(objUsers);
                    if (userAlreadyAvailable == null)
                    {
                        objUsers.Password = HashPassword.GetHashPassword(objRequest.Password);
                        _blogRepo.Register(objUsers);

                        if (!string.IsNullOrEmpty(objRequest.ProfilePic))
                        {
                            //Commented on 9 sep 2023
                            string BasePath = _config["AppBasePath"].ToString();
                            //string RelativeFolderPath = "\\Files\\ProfilePic\\";

                            //string BasePath = Path.Combine("");
                            string RelativeFolderPath = "\\Files\\ProfilePic\\";                           

                            string FilePath = (new CommonUtility()).SaveMediaFile(objUsers.UserId, objRequest.ProfilePic, BasePath, RelativeFolderPath);

                            if (!string.IsNullOrEmpty(FilePath))
                            {
                                _blogRepo.Register_UpdateProfilePic(objUsers.UserId, FilePath);
                            }
                        }

                        string test = response.ToString();

                        response = Ok(new RegisterResponse()
                        {
                            StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                            StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                            Description = ValidationMessages.Register.UserRegistered
                        });
                    }
                    else
                    {
                        response = Ok(new RegisterResponse()
                        {
                            StatusCode = ValidationMessages.HttpRequestCode.ErrorCode,
                            StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                            Description = ValidationMessages.Register.AlreadyRegistered
                        });
                    }
                }
                else
                {
                    response = Ok(new RegisterResponse()
                    {
                        StatusCode = ValidationMessages.HttpRequestCode.ErrorCode,
                        StatusMessage = ValidationMessages.HttpRequestCode.ErrorMsg,
                        Description = ValidationMessages.HttpRequestCode.AllMandatoryParameter
                    });
                }
            //}
            //catch (Exception ex) 
            //{
            //    ErrorLog objError = new ErrorLog()
            //    {
            //        UserId = null,
            //        ControllerName = "Account",
            //        ActionName = "Register",
            //        RequestTime = DateTime.Now,
            //        JSONRequest = CommonUtility.ConvertObjectToJSON(objRequest),
            //        ErrorStackTrace = ex.Message + "--" + ex.StackTrace,
            //        ClientIP = CommonUtility.GetClientIPAddress()
            //    };

            //    _blogDB.tblErrorLog.Add(objError);
            //    _blogDB.SaveChanges();
            //}            
            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCountry")]
        public IActionResult GetCountry()
        {
            IActionResult response = null;

            List<Country> lstCountry = _blogRepo.GetCountry();
            if (lstCountry != null)
            {
                List<CountryList> lstCountryList = new List<CountryList>();
                if (lstCountry != null && lstCountry.Count > 0)
                {
                    foreach (Country obj in lstCountry)
                    {                         
                        lstCountryList.Add(
                        new CountryList()
                        {
                            CountryId = obj.CountryId,
                            Countryname = obj.Countryname
                        });
                    }
                }

                response = Ok(new CountryResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                    Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                    lstDetails = lstCountryList
                });
            }
            else
            {
                response = Ok(new CountryResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                    Description = ValidationMessages.HttpRequestCode.NoRecordFound
                });
            }

            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetState")]
        public IActionResult GetState()
        {
            IActionResult response = null;
                        
            List<State> lstState = _blogRepo.GetState();
            if (lstState != null)
            {
                response = Ok(new StateResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                    Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                    lstDetails = lstState
                });
            }
            else
            {
                response = Ok(new StateResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                    Description = ValidationMessages.HttpRequestCode.NoRecordFound
                });
            }            
            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCity")]
        public IActionResult GetCity(long id)
        {
            IActionResult response = null;
            List<City> lstCity = _blogRepo.GetCity(id);

            if (lstCity != null)
            {
                response = Ok(new CityResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                    Description = ValidationMessages.HttpRequestCode.SuccessDescription,
                    lstDetails = lstCity
                });
            }
            else
            {
                response = Ok(new CityResponse()
                {
                    StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                    StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                    Description = ValidationMessages.HttpRequestCode.NoRecordFound
                });
            }
            return response;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("GetSampleResult")]
        public IActionResult GetSampleResult()
        {
            IActionResult response = null;
            response = Ok(new Response()
            {
                StatusCode = ValidationMessages.HttpRequestCode.SuccessCode,
                StatusMessage = ValidationMessages.HttpRequestCode.SuccessMsg,
                Description = ValidationMessages.HttpRequestCode.NoRecordFound
            });
            return response;
        }

        //[NonAction]
        //private string GenerateToken(Users objUser)
        //{
        //    // generate token that is valid for 7 days
        //    //var tokenHandler = new JwtSecurityTokenHandler();
        //    //var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

        //    //var claims = new List<Claim>()
        //    //{
        //    //    new Claim("UserId", objUser.UserId.ToString()),
        //    //    new Claim("Username", objUser.Username),
        //    //    new Claim("Firstname", objUser.Firstname),
        //    //    new Claim("Lastname", objUser.Lastname),
        //    //    new Claim("Mobile", objUser.Mobile.ToString()),
        //    //    new Claim("ProfilePicPath", objUser.ProfilePicPath.ToString()),
        //    //};

        //    //var tokenDescriptor = new SecurityTokenDescriptor
        //    //{
        //    //    Subject = new ClaimsIdentity(new[] { new Claim("Username", objUser.Username) }),
        //    //    //Claims = claims,
        //    //    Expires = DateTime.UtcNow.AddSeconds(30),
        //    //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    //};

        //    //var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //return tokenHandler.WriteToken(token);

        //    var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

        //    var claims = new List<Claim>()
        //    {
        //        new Claim("UserId", objUser.UserId.ToString()),
        //        new Claim("Username", objUser.Username),
        //        new Claim("Firstname", objUser.Firstname),
        //        new Claim("Lastname", objUser.Lastname),
        //        new Claim("Mobile", objUser.Mobile.ToString())
        //    };

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
        //        expires: DateTime.Now.AddSeconds(30), signingCredentials: credentials
        //        );
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}


    }
}
