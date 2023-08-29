using ApplicationBlog.Controllers;
using ApplicationBlog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationBlog.Utility;
using ApplicationBlog.DBContext;
using AccountController_Test;

namespace ApplicationBlog_XUnitTest
{
    public class AccountController_Test
    {
        BlogDbContext dbContext = null;
        private IBlogRepository _blogRepo;
        private AccountController CreateAccountController()
        {
            _blogRepo = new MockBlogRepository();

            /***** Using In Memory Configuration *****/
            if (dbContext == null)
            {
                var options = new DbContextOptionsBuilder<BlogDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
                dbContext = new BlogDbContext(options);
            }

            var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"Jwt:Key", "qwertyuiopasdfghjklzxcvbnm"},
                {"Jwt:Issuer", "http://ApplicationBlog.com"},
                {"Jwt:Audience", "http://ApplicationBlog.com"},
                {"AppBasePath", "C:\\Pramod\\Project\\AppBlog_React\\blog_app\\public"}
            })
            .Build();
            return new AccountController(configuration, dbContext);
        }

        [Theory]
        [InlineData("DemoUser@gmail.com", "1234", "200")]
        [InlineData("DemoUser@gmail.com", "12345", "500")]        
        public async Task AccountController_Login(string username, string password, string expectedResult)
        {            
            var authController = CreateAccountController();
            var user = new Users
            {
                Username = "DemoUser@gmail.com",
                Password = HashPassword.GetHashPassword("1234"), // Password should be hashed in a real application
                Firstname = "Demo",
                Lastname = "User",
                Mobile = "9365412541",
                Gender = 'M',
                DOB = DateTime.Parse("2020-05-23"),
                ProfilePic = string.Empty,
                BackgroundPic = string.Empty,
                ProfileStatus = ValidationMessages.Register.ProfileDefaultStatus,
                CountryId = Convert.ToInt32("1"),
                RegisteredOn = DateTime.Now,
                IsActive = true
            };

            dbContext.tblUsersMaster.Add(user);
            await dbContext.SaveChangesAsync();
            
            var userModel = new Login { Username = username, Password = password };

            var result = authController.Login(userModel) as OkObjectResult;
            var StatusCode = ((LoginResponse)result.Value).StatusCode;

            //Assert.NotNull(StatusCode);
            Assert.Equal(expectedResult, StatusCode);
        }

        [Theory]
        [InlineData("DemoUser@gmail.com", "1234", "Demo", "User", "9876543210", 'M', "2023-08-10", "1", "500")]
        [InlineData("TestingUser@gmail.com", "1234", "Testing", "User", "9874563210", 'M', "2023-08-10", "1", "200")]        
        public async Task AccountController_Register(string username, string password, string firstname, string lastname, string mobile, char gender, string dob, string country, string expectedResult)
        {            
            var authController = CreateAccountController();
            var user = new Users
            {
                Username = "DemoUser@gmail.com",
                Password = HashPassword.GetHashPassword("1234"),
                Firstname = "Demo",
                Lastname = "User",
                Mobile = "9365412541",
                Gender = 'M',
                DOB = DateTime.Parse("2020-05-23"),
                ProfilePic = string.Empty,
                BackgroundPic = string.Empty,
                ProfileStatus = ValidationMessages.Register.ProfileDefaultStatus,
                CountryId = Convert.ToInt32("1"),
                RegisteredOn = DateTime.Now,
                IsActive = true
            };

            dbContext.tblUsersMaster.Add(user);
            await dbContext.SaveChangesAsync();
            
            var userModel = new Register { Username = username, Password = password, Firstname = firstname, Lastname = lastname, Mobile = mobile, Gender = gender, DOB = dob, CountryId = country };

            var result = authController.Register(userModel) as OkObjectResult;
            var StatusCode = ((RegisterResponse)result.Value).StatusCode;

            Assert.Equal(expectedResult, StatusCode);
        }        
    }
}

/***** Using Moq Configuration *****/
//var mockDbContext = new Mock<BlogDbContext>(new DbContextOptions<BlogDbContext>());

//var mockConfig = new Mock<IConfiguration>();
//mockConfig.SetupGet(x => x["Jwt:Key"]).Returns("qwertyuiopasdfghjklzxcvbnm");
//mockConfig.SetupGet(x => x["Jwt:Issuer"]).Returns("http://ApplicationBlog.com");
//mockConfig.SetupGet(x => x["Jwt:Audience"]).Returns("http://ApplicationBlog.com");

//return new AccountController(mockConfig.Object, mockDbContext.Object);

//using var dbContext = new BlogDbContext(new DbContextOptionsBuilder<BlogDbContext>()
//    .UseInMemoryDatabase(databaseName: "TestDatabase")
//    .Options);