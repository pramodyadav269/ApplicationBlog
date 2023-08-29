namespace ApplicationBlog.Utility
{
    public class ValidationMessages
    {
        public class HttpRequestCode
        {
            public static string SuccessCode = "200";
            public static string ErrorCode = "500";
            public static string UnauthorizedCode = "401";

            public static string SuccessMsg = "success";
            public static string ErrorMsg = "error";
            public static string UnauthorizedMsg = "Unauthorize Request";

            public static string SuccessDescription = "Process completed successfully";
            public static string AllMandatoryParameter = "Mandatory parameters are required";
            public static string DataValidation = "Data validation error occured";
            public static string NoRecordFound = "No Record Found";

            public static string TechnicalError = "It seems something went wrong, please try again.";
        }
        public class Login
        {
            public static string SuccessLogin = "User loged in successfully";
            public static string InvalidCredentials = "Invalid Credentials";
        }
        public class Register
        {
            public static string UserRegistered = "User registered successfully";
            public static string AlreadyRegistered = "User already exists gainst given Username/Mobile";
            public static string ProfileDefaultStatus = "Hey there! I am using LTI blog";
        }
        public class GetAllPost
        {
            public static string PostNotFound = "No posts are available";
        }
        public class SubmitUserPost
        {
            public static string PostSubmitted = "Content submitted successfully";
        }
        public class PostType
        {
            public static string Text = "text";
            public static string Image = "image";
            public static string Video = "video";
        }

        public class Procedure
        {
            public static string usp_GetPostComments = "usp_GetPostComments";
            public static string usp_SubmitPostLike = "usp_SubmitPostLike";

        }
    }
}
/*
 ***** React App URL :- https://applicationblog-74308.web.app/login *****

1. Go to Application root directory
2. Run "npm install -g firebase-tools" command (note :- npm must be installed in system)
3. Go to "https://firebase.google.com/" url and login with email account then run this 	"firebase login" in VS code terminal
4. We Will get login successfull msg on browser
5. execute "firebase init" command
6. Are you ready to proceed? "Yes"
7. select "Hosting: Configure files for firebase hosting and (optionally) set up github action 	deploys" option(first use arrow button then press '*' button to select then press 	"enter" button)
8. Please select an option: "Use an existing project"
9. Select website which we have created(ProjectId)
10.What do you want to use as your public directory? "build"
11.Configure as a single-page app (rewrite all urls to /index.html)? "Yes"
12.Set up automatic builds and deploys with GitHub? "No"

*** firebase initialization will complete here ***

13.npm run build
14.firebase deploy --only hosting

*** Application deployed on firebase ***

15.Copy Hosting URL and check in browser


***** For further changes *****
1. After code change in project linked with firebase system open terminal in VS code and type
	"npm run build" command
2.then execute "firebase serve" command to deploy project locally to test the 	changes(Optioanl)
3.At last execute "firebase deploy" command to deploye changes on firebase server


 */