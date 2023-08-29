using ApplicationBlog.Controllers;
using ApplicationBlog.DBContext;
using ApplicationBlog.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountController_Test
{
    public class HomeController_Test
    {
        BlogDbContext dbContext = null;
        private IBlogRepository _blogRepo;
        private AccountController CreateHomeController()
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


        public void HomeController_GetAllPost(long? id)
        {
            
        }
    }
}
