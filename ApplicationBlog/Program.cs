using ApplicationBlog.Middlewares;
using ApplicationBlog.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //Below commented 3 line of code for production
            //ValidateIssuer = true,
            //ValidateAudience = true,
            //ValidateLifetime = true,

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

//builder.Services.AddTransient<CustomExceptionLogging>();
// Provide a factory function to create an instance of CustomExceptionLogging with the required dependencies
//builder.Services.AddTransient<CustomExceptionLogging>(serviceProvider =>
//{
//    var blogDbContext = serviceProvider.GetRequiredService<BlogDbContext>();
//    return new CustomExceptionLogging(next => new CustomExceptionLogging(next, blogDbContext));
//});

builder.Services.AddCors();

//CORS for all users
//builder.Services.AddCors(policyBuilder => 
//    policyBuilder.AddDefaultPolicy(policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
//);

//CORS for specific users
//builder.Services.AddCors(policyBuilder =>
//    policyBuilder.AddDefaultPolicy(policy => policy.WithOrigins("https://applicationblog-85678.web.app").AllowAnyHeader().AllowAnyMethod())
//);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BlogDbContext>(item => item.UseSqlServer(builder.Configuration["ConnectionStrings:ApplicationBlog"]));

var app = builder.Build();

//This is to open default page when application is browsing with domain name only
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicationBlog v1");
    c.RoutePrefix = string.Empty;
});

//For localhost to allow cross-origin request
app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
                                                //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
            .AllowCredentials()); // allow credentials

//For PROD to allow request from specific application
//app.UseCors(builder =>
//{
//    builder
//           .WithOrigins(new string[] { "https://applicationblog-85678.web.app" })
//           .AllowAnyMethod()
//           .AllowAnyHeader();    
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CustomExceptionLogging>();
app.MapControllers();

app.Run();
