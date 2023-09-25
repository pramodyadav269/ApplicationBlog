using ApplicationBlog.Model;
using ApplicationBlog.Utility;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace ApplicationBlog.Middlewares
{
    public class CustomExceptionLogging
    {
        private readonly RequestDelegate _next;
        //private readonly BlogDbContext _blogDB;
        private readonly IServiceScopeFactory _scopeFactory;

        public CustomExceptionLogging(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            //_blogDB = blogDB;            
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {                
                await _next(context);
            }
            catch (Exception ex)
            {
                // Create a scope to resolve the DbContext
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _blogDB = scope.ServiceProvider.GetRequiredService<BlogDbContext>();

                    // Log the exception to the SQL Server table
                    await LogExceptionAsync(_blogDB, context, ex);
                }

                // Send a custom error response to the client
                await HandleExceptionAsync(context, ex);

                // Re-throw the exception to let the global exception handler handle it
                //throw;
            }
        }

        private async Task LogExceptionAsync(BlogDbContext _blogDB, HttpContext context, Exception ex)
        {            
            var requestBody = await ReadRequestBodyAsync(context.Request);
            string Message = ex.Message;
            string StackTrace = ex.StackTrace;
            DateTime LogTime = DateTime.UtcNow;
            string RequestMethod = context.Request.Method;
            string RequestPath = context.Request.Path;
            string RequestQueryString = context.Request.QueryString.ToString();
            string RequestHeaders = SerializeHeaders(context.Request.Headers);
            string RequestBody = requestBody;

            var objError = new ErrorLog
            {
                UserId = null,
                ControllerName = "Account",
                ActionName = "Register",
                RequestTime = DateTime.Now,
                JSONRequest = "",//CommonUtility.ConvertObjectToJSON(objRequest),
                ErrorStackTrace = ex.Message + "--" + ex.StackTrace,
                ClientIP = CommonUtility.GetClientIPAddress()
            };

            _blogDB.tblErrorLog.Add(objError);
            await _blogDB.SaveChangesAsync();
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            using (var reader = new StreamReader(request.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }
        private string SerializeHeaders(IHeaderDictionary headers)
        {
            // Serialize the headers to a string or format as needed
            // For example, you can use JSON serialization
            return JsonConvert.SerializeObject(headers);
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Create a custom error response JSON or any format you prefer
            var errorResponse = new
            {
                StatusCode = "500",
                StatusMessage = "An error occurred while processing your request.",
                Description = ex.Message,
                // You can include additional information in the response if needed
            };

            // Serialize the error response to JSON
            var jsonResponse = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            // Write the JSON response to the client
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
