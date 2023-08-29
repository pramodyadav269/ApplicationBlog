using Microsoft.EntityFrameworkCore;

namespace ApplicationBlog.Model
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
            
        }        
        public DbSet<Users> tblUsersMaster { get; set; }
        public DbSet<UserPost> tblUserPost { get; set; }
        public DbSet<UserPostLike> tblUserPostLike { get; set; }
        public DbSet<UserPostComment> tblUserPostComment { get; set; }
        public DbSet<UserProfilePic> tblUserProfilePic { get; set; }
        public DbSet<State> tblStateMaster { get; set; }
        public DbSet<City> tblCityMaster { get; set; }
        public DbSet<Country> tblCountryMaster { get; set; }
        public DbSet<FriendRequest> tblFriendRequest { get; set; }

        public DbSet<ErrorLog> tblErrorLog { get; set; }
    }
}


/*
 using Xunit;
using Moq;
using ApplicationBlog.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationBlog.Controllers;
using ApplicationBlog.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Json;
using System.Reflection;

namespace ApplicationBlog_XUnitTest
{
    public class AccountController_Test
    {        
        [Fact]        
        public void AccountController_Login_ReturnsOkWithToken()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BlogDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var context = new BlogDbContext(options);
            //context.tblUsersMaster.Add(new Users { Username = "pramodyadav269@gmail.com", Password = "1234" });
            //context.SaveChanges();

            var config = new Mock<IConfiguration>();
            //config.Setup(c => c.GetConnectionString("DefaultConnection")).Returns("InMemoryDatabase");
            //config.Setup(c => c.GetConnectionString("ApplicationBlog")).Returns("InMemoryDatabase");
            config.Setup(c => c.GetConnectionString("ApplicationBlog"))
                         .Returns("Server=DESKTOP-A50CJ4V;Database=ApplicationBlog;Trusted_Connection=True; MultipleActiveResultSets=true");

            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("qwertyuiopasdfghjklzxcvbnm");

            var controller = new AccountController(config.Object, context);

            //var configurationField = typeof(AccountController).GetField("_configuration", BindingFlags.Instance | BindingFlags.NonPublic);
            //configurationField.SetValue(controller, config.Object);

            var loginRequest = new Login { Username = "pramodyadav269@gmail.com", Password = "1234" };

            // Act
            var result = controller.Login(loginRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            
            //Assert.NotNull(result);
            //Assert.Equal(200, result.StatusCode);
        }

        //private readonly AccountController _controller;
        //private readonly Mock<IConfiguration> _configMock;
        //private readonly Mock<BlogDbContext> _dbContextMock;

        //public AccountController_Test()
        //{
        //    _configMock = new Mock<IConfiguration>();
        //    _dbContextMock = new Mock<BlogDbContext>();
        //    _controller = new AccountController(_configMock.Object, _dbContextMock.Object);
        //}
        //[Fact]
        //public void Login_ValidCredentials_ReturnsOkWithToken()
        //{
        //    // Arrange
        //    var user = new Users
        //    {
        //        UserId = 1,
        //        Username = "testuser",
        //        Firstname = "Test",
        //        Lastname = "User",
        //        Mobile = "1234567890",
        //        IsActive = true
        //    };
        //    //var users = new List<Users>
        //    //{
        //    //    new Users
        //    //    {
        //    //        UserId = 1,
        //    //        Username = "testuser",
        //    //        Password = HashPassword.GetHashPassword("1234"),
        //    //        IsActive = true
        //    //    }
        //    //}.AsQueryable();

        //    var loginRequest = new Login { Username = "pramodyadav269@gmail.com", Password = "1234" };

        //    var usersData = new List<Users> { user };
        //    var usersMock = MockDbSet(usersData.AsQueryable());

        //    _dbContextMock.Setup(db => db.tblUsersMaster)
        //        .Returns(usersMock);

        //    // Act
        //    var result = _controller.Login(loginRequest) as ObjectResult;

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);
        //    var responseData = result.Value as LoginResponse;
        //    Assert.NotNull(responseData);
        //    Assert.Equal(ValidationMessages.HttpRequestCode.SuccessCode, responseData.StatusCode);
        //    Assert.Equal(user.ProfilePic, responseData.ProfilePic);
        //    Assert.NotNull(responseData.Token);
        //}

        //public static DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
        //{
        //    var mockSet = new Mock<DbSet<T>>();
        //    mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        //    return mockSet.Object;
        //}

        //[Fact]
        //public void Login_InvalidCredentials_ReturnsOkResultWithErrorMessage()
        //{
        //    var configMock = new Mock<IConfiguration>();
        //    var dbContextOptions = new DbContextOptionsBuilder<BlogDbContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDatabase")
        //        .Options;
        //    var dbContextMock = new Mock<BlogDbContext>(dbContextOptions);

        //    // Arrange
        //    var controller = new AccountController(configMock.Object, dbContextMock.Object);
        //    var loginRequest = new Login { Username = "invaliduser", Password = "invalidpassword" };

        //    var users = new List<Users>
        //    {
        //        new Users
        //        {
        //            UserId = 1,
        //            Username = "pramodyadav269@gmail.com",
        //            Password = HashPassword.GetHashPassword("1234"),
        //            IsActive = true
        //        }
        //    }.AsQueryable();

        //    var usersDbSetMock = new Mock<DbSet<Users>>();
        //    usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(users.Provider);
        //    usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(users.Expression);
        //    usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(users.ElementType);
        //    usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());


        //    // TODO: Set up your mock DbContext to return null for the user
        //    dbContextMock.Setup(db => db.tblUsersMaster).Returns(usersDbSetMock.Object);

        //    // Act
        //    var result = controller.Login(loginRequest);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var loginResponse = Assert.IsType<LoginResponse>(okResult.Value);
        //    Assert.Equal(ValidationMessages.HttpRequestCode.ErrorCode, loginResponse.StatusCode);
        //    Assert.Equal(ValidationMessages.Login.InvalidCredentials, loginResponse.Description);
        //}
    }

}

 */