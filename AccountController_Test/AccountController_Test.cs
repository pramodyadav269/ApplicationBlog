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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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


/*
1.
Explain the 2 methods in Program.cs class
Handle exception in .netcore
Where the errors are logged in your application
How to handle session in your .netcore application
How your application is deployed
Transient vs Scoped
Usestate
Useeffect
Azure event hubs
Azure functions
Azure service bus
Primary key vs unique key
CTE
Stored procedures

2.
what is sealed class
What is the difference between field & properties?
How parameterized constructor of base class can be called from derived class?

public DerivedClass(String Value) : Base(Value)
{

}

1)public interface I3DShape{

   double SurfaceArea{get;}

   double Volume{get;}

}

 

public class Sphere: I3DShape

{

   private readonly double _radius;

 

   public Sphere(double diameter){

       _radius = diameter/2;

   }

   public double SurfaceArea

   {

       get { return 4 * Math.PI * Math.Pow(_radius,2); }

   }

   public double Volume

   {

       get { return (4/3) * Math.PI * Math.Pow(_radius, 3); }

   }

}
In above example tell me what is abstraction and encapsulation 

2) output of code
class A

{

  public virtual void test()

   {

       Console.WriteLine("A:test");

   }

}

class B:A

{

   public new void test()

   {

       Console.WriteLine("B:test");

   }

}

class C : A

{

   public override void test()

   {

       Console.WriteLine("C:test");

   }

}

internal class Program

   {

       static void Main(string[] args)

       {

           A a1 = new B();

           A a2 = new C();

           a1.test();

           a2.test();

       }

   }




3)Write an expression to print the same sentence such that each word in the sentence is reversed without changing the position of the words within the sentence
string str = "a quick brown fox jumped over a lazy dog";   
//var tempStr =str.Split();



/*for(int i=tempStr.Length-1;i>=0;i--)
{
console.write(tempStr[i]+ " ");
 newStr.Append(tempStr[i]+ " ");

}

console.writeLine(newStr);

var tempStr = str.Split();

stringBuilder newStr = new stringBuilder();

for (int i = 0; i < tempStr.Length - 1; i++)
{
    char[] charArray = tempStr[i].TocharArray(); a kciuq

    for (int j = charArray - 1; j >= 0; j--)
    {
        newStr.Append(charArray[i]);
    }
    newStr.Append(" ");
}



4) extract the following data Designation, MinCompensation, MaxCompensation, AvgSalary 

a. Employee table (id INT, name NVARCHAR(100), DesignationId INT, address NVARCHAR(500), phone_number NVARCHAR(15), doj DATETIME)

b.Designation(id INT, title NVARCHAR(100), minCompensation Decimal, maxCompensation Decimal)

c.EmployeeCompensation(id INT, EmployeeId INT, BasicPay Decimal, VariablePay Decimal, PF Decimal, Gratuity Decimal, TotalSalary Decimal[calculated column])

Designation, MinCompensation, MaxCompensation, AvgSalary 

select d.title, d.MinCompensation, d.MaxCompensation, AVG(TotalSalary  ) from Employee E
join designation d on d.id=e.DesignationId
join EmployeeCompensation  EC on EmployeeId.EC=e.id
group by d.title, d.MinCompensation, d.MaxCompensation


3.
input string
hello word

duplicate character with index poistion as well


var name = "Hello World";
var lowerName = name.ToLower();
while (lowerName.Length > 0)
{
   string indexes =null;
    
    for (int i = 0; i < lowerName.Length; i++)
    {
        if (lowerName[i].ToString() !=" ")
        {
            char ch = lowerName[i];
            int count = lowerName.Where(x => (x == lowerName[i])).Count();       
            if (count > 1)
            {

                for (int j=0;j < name.Length; j++) {
                    if (lowerName[i] == name[j])
                    {
                        //int index = name.IndexOf(name[j]);
                         indexes =string.Concat(indexes, j);
                    }
                }
                

                Console.WriteLine(lowerName[i]  + " index position :"+ indexes + " Character count : " + count);
                lowerName = lowerName.Replace(lowerName[i].ToString(), String.Empty);
            }
        }
    }


2) reverse print strinf of same hello world
3) 

3)datareaader and dataAdapter
4)Ienurambela and Iquerable 



table called city

Cid cname
 no duplicate values , should have 


with ctename
as
(select cname , row_number() over (partition by cname) as 'rid' from city  )



delete from ctename where rid > 1

1 3 row 
2 3 rows inner jion 
how many rows


use of ## and CTE

where is memeory allocate for ## and Cte

we have 3 transcartions tables ,each trns depons each other, dml store proced,or order

exec commit
====

frontent

react js

diff b/w class backed and functinoal  

updatedidmount , how to 

Azure js

previous to track 
 

initalize instances,
me

propertties and field

Int, int

difference b/w fields variable b/w property

can i declared property in a interface
can i declre field variable in interface ?
what is difference b/w interface & class ?
can u tell diff b/w 
encapsulation & abstraction 


class Encap{

Public string _variable;

public void Greet(){

  console.writeline()

}

}
====================
class A

{

  public virtual void test()

   {
       Console.WriteLine("A:test");
   }
}

class B:A

{
   public new void test()
   {
      Console.WriteLine("B:test");
   }
}
class C : A
{
   public override void test()
   {
       Console.WriteLine("C:test");
   }
}

internal class Program
   {
       static void Main(string[] args)
       {
           A a1 = new B();
           A a2 = new C();
           a1.test();
           a2.test();

       }

   }

===============
dependency injection types ?

interface

creating depencdency injection 


public Iinterface _Interface;

public depencey(Iinteface interface)
{
Interface =Interface
}

declaring container


==========================

string str = "a quick brown fox jumped over the lazy dog"; 
 

var str1 = str.split(' ');
var strFinal = string.Empty;
foreach(var eC in str1){

    var str3 =ec.tocharArray();

     for(int i= 0 i < str3.length; i--){

         strFinal = strFinal +str[i];

      }
       strFinal = strFinal + " ";
}
console.WriteLine(strFinal);


==================================

string str = "a quick brown fox jumped over the lazy dog"; 
 
 var str1 = str.where(x=>(x)).spl
var str1 = str.split(' ');
var strFinal = string.Empty;
foreach(var eC in str1){

    var str3 =ec.tocharArray();

     for(int i= 0 i < str3.length; i--){

         strFinal = strFinal +str[i];

      }
       strFinal = strFinal + " ";
}
console.WriteLine(strFinal);


=========================================
a. Employee table (id INT, name NVARCHAR(100), DesignationId INT,address NVARCHAR(500), phone_number NVARCHAR(15), doj DATETIME)

b. Designation (id INT, title NVARCHAR(100), minCompensation Decimal, maxCompensation Decimal)

c. EmployeeCompensation (id INT, EmployeeId INT, BasicPay Decimal, VariablePay Decimal, PF Decimal, Gratuity Decimal, TotalSalary Decimal [calculated column])

i want to prepare employee salary with band min max salary , the particular employee 

Designation, MinCompensation, MaxCompensation, AvgSalary


select d.title,d.MinCompensation,d.MaxCompensation ,Avg( ec.TotalSalary) from employee e innerjoin designation d on e.DesignationId =d.id innerjoin EmployeeCompensation ec on ec.EmployeeId =e.id group by (d.title)


==================================
 console.log(10+5+"Test"+5+5);

             15Test10
string[] arr = [a,b,c,d,e];

arr.slice(2,4);
arr.unshift(0)
.console.log(arr.unshift(0));

=====================================

string[] arr = [a,b,c,d,e];

for (let i of arr) {
 console.log(i)
} 
a,b,c,d,e

=================


-----------------------Renuka

1. Logic program on c#
identity the longest palendrome
given string many palendrome , i have to find the longest palendrome 


2 tables


employee and salary tables

with empcte
as(
  select e.eid,e.name,s.salary, count(*) from empoylee e inner join salary s on e.id =s.id where e.eid%2!=0
)

select * from empcte


two tables ,want count b/w tables
========================================================

ex 100 rec in tab1 ,60 rec tab 2

with empcte
as(
  select count(*) from table1 t1 inner join table2 t2 on t1.id =t2.id where t2.eid not in(t1.eid) //t2.eid is null
)

select * from empcte

select top1 * from (select top3* from employee order by salary desc) order by salary


select * from ( select distinct(salary), Dense_Rank() over(order by salary desc) as 'rid'from emp) where rid = 3
======================================================================
call api from the browser ,certain tool api into select statement entites




create table emp(empid int, ename varchar(50), check constraint constrainname(salary <=10000))

audit logs implement 

want to montor the logs and check the audit trail

i have req web application in dashdasboard min max, avg salary ,employees list in single communication to db, how can we do 

complet table structure.



4.

I appeared for the Microsoft interview today(07-07-2023) and I have mentioned the asked questions below:

Technical Discussions :
1) Difference between HTTP Post and HTTP Put in Webapi ?
2) What are the OOPs concepts ?
3) SQL Indexes Clustered and Non clustered Index ?
4) What is token authentication ?
5)Write a query to change the Gender from Male to Female and Vice Versa in the Employee table?
6)Find the 3rd order value in the array ?
7) Diff b/w View Data & View Bag in MVC?
8) Action Filters in MVC ?
9) Diff b/w Abstract and Interface ?
10) Shall we use multiple inheritance in C# ?
11) What is token authentication ?
12)what is custom filter in MVC ?
13) Why is interface used in C#?
14) Explain Solid Principle ?


5.

1.	heap vs stack
2.	readonly vs constant
3.	what is delegates?
4.	What are extension methods?
5.	How do you authenticated your API?
6.	String vs string builder
7.	Mutable vs Immutable
8.	Routing in mvc
9.	What is attribute routing
10.	MVC lifecycle
11.	What is refresh token
12.	Directives in angular
13.	Program to find 3rd highest number in Array
14.	Program to find factorial of number using recursion
15.	What is runtime runtime binding
16.	What is static
17.	When to use interface and when to use abstract class
18.	Difference between static and singletone
19.	Can we write multiple catch block with try
20.	How to log exceptions


6.

1. what type of work you worked in previous project related to sql?
2. stored procedures and optimization
3. how handle merge query if we have millions of records, any optimizations techniques to follow?
4. DDL and DML statements in sql
5. what are triggers in sql
6. swap two numbers without using temp variable
7. fibonacci series
8. sealed classes
9. diff b/w abstract class and interface
10. explain MVC architecture
11. How to handle bugs/ code is not working in production.
12. Multilevel inheritance
13. Token authentication mechanism.

*/


/* AWS Assignment
 
Problem Statement
Building a Scalable and Reliable Web Application on AWS

Description
You are tasked with architecting and implementing a web application hosted on AWS that needs to be highly scalable, reliable, and maintainable. The application should handle a significant number of concurrent users and traffic fluctuations.

User Stories
1.	You are to design the Solution Architecture for the demo system.
2.	Frontend: Deploy a static website on AWS S3 and serve it via AWS CloudFront.
3.	Backend: Deploy a highly available and resilient Nginx Service with AWS EC2 Services
4.	Setup CI/CD Pipeline using GitHub & AWS CodePipeline for the static website described in #2
5.	Setup Observability solutions 
a.	Monitor the health of various infrastructure components through a custom built CloudWatch Dashboard e.g.: AWS EC2, AWS ELB.
b.	Creating alarms for AWS Service metrics ego: 5xx errors exceeds a threshold limit.

Guidelines
6.	Infrastructure as Code (IaC): Use AWS CloudFormation to provision and manage the required resources.
7.	High Availability: Implement an architecture that ensures high availability across multiple availability zones to prevent single points of failure.
8.	Auto Scaling: Configure auto-scaling policies to automatically adjust resources based on traffic load.
9.	Monitoring and Alerting: Set up monitoring for key performance metrics and configure alerts to notify when thresholds are breached.
10.	Logging and Troubleshooting: Implement logging mechanisms to assist in debugging and troubleshooting issues.
11.	Continuous Integration/Continuous Deployment (CI/CD): Create a CI/CD pipeline that automatically deploys changes to the application while ensuring safety and minimizing downtime.
12.	Security and Access Control: Implement appropriate security measures, including IAM roles, security groups, and encryption, to safeguard the application and data.

Evaluation
You will be evaluated based on your ability to design a scalable, reliable, and maintainable infrastructure on AWS. Key aspects include proper use of AWS services, adherence to best practices, automation, and security considerations. Additionally, you will be assessed on your approach to handling failure scenarios and troubleshooting capabilities.

Q -> install nginx on windows server 2022 using user data script..
A ->
<powershell>
# Install Chocolatey (a package manager for Windows)
Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

# Install Nginx using Chocolatey
choco install nginx -y
</powershell>
*/

/* AWS CI/CD with react App steps

1. Create S2 bucket with bucket policy

{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Sid": "PublicReadGetObject",
            "Effect": "Allow",
            "Principal": "*",
            "Action": [
                "s3:GetObject",
                "s3:GetObjectVersion"
            ],
            "Resource": "arn:aws:s3:::applicationblog-react-aws-s3/*"
        },
        {
            "Sid": "Stmt1597860486271",
            "Effect": "Allow",
            "Principal": {
                "AWS": "arn:aws:iam::700186024813:role/service-role/codebuild-buildproject_blog_app-service-role"
            },
            "Action": "*",
            "Resource": "arn:aws:s3:::applicationblog-react-aws-s3/*"
        }
    ]
}

2. Create CloudFront Distributor
3. Browse appliation with distributor name. if application is throwing "Access Denied" error then go to CloudFront and under ErrorPages "create custom error response" 
4. Now create "CodePipeline" and click on "Create Pipeline" and skip deployment stage for time being.
   we'll get build failed error so click on "Edit stage" then click on edit icon in AWS CodeBuild 
*/


/********* .NetCore applciation 
 


EC2 instance details:-

34.207.104.189
Administrator
voZZNphEnvL$&zusdF20YFFmjPt((VP&
 
 
https://labs.sogeti.com/deploy-net-core-web-api-to-linux-ubuntu/	(Reference Link)
https://www.youtube.com/watch?v=H34jAKxqnmQ				(Reference video)

3.93.10.174 (EC2 instance public IP)

ubuntu	(Username to access server)

Reference :- https://www.youtube.com/watch?v=ll-1O-3IBmY


sudo su - (to switch user as switch user)

sudo apt-get update

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb

rm packages-microsoft-prod.deb

sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-7.0

dotnet

sudo apt-get install nginx

sudo service nginx start

/etc/nginx/sites-available/default

vi default

ssh -i "D:\Cloud\AWS\ubuntu\pramod_ubuntu.pem" ubuntu@ec2-3-93-10-174.compute-1.amazonaws.com

i

server {
    listen        80;
    server_name   YOUR_DOMAIN;
    location / {
        proxy_pass         http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}

esc

:wq


 */


/*
create table tblLeadSourceMapping(Id bigint identity(1,1) not null,TokenSource varchar(50),LeadSource varchar(50),FilterKey varchar(50))
insert into tblLeadSourceMapping values('OneCRM','HomePage')
insert into tblLeadSourceMapping values('OneCRM','Microsite')
insert into tblLeadSourceMapping values('OneCRM','ABC')

create table tblCampaignMapping(Id bigint identity(1,1),LSMap bigint,Parameter varchar(50),Value varchar(50),ParentId bigint,CampaignId varchar(50))
insert into tblCampaignMapping values(1,'BusinessName','LifeInsurance',null,'1001')
insert into tblCampaignMapping values(2,'BusinessName','LifeInsurance',null,'1002')
insert into tblCampaignMapping values(2,'UTM','UTMSet1',null,'1003')
insert into tblCampaignMapping values(2,'UTM','UTMSet2',null,'1004')
insert into tblCampaignMapping values(2,'BusinessName','MutualFunds',3,null)
insert into tblCampaignMapping values(2,'BusinessName','MutualFunds',4,null)
insert into tblCampaignMapping values(3,'Pincode','PincodeSet1',null,'1005')
insert into tblCampaignMapping values(3,'Pincode','PincodeSet1',null,'1006')
insert into tblCampaignMapping values(3,'ProductCode','ProductCode1',7,null)
insert into tblCampaignMapping values(3,'ProductCode','ProductCode2',8,null)

select * from tblLeadSourceMapping
select * from tblCampaignMapping

declare @LeadSource varchar(50)='ABC',@TokenSource varchar(50)='OneCRM'
,@json_data NVARCHAR(MAX)= '{"BusinessName": "MutualFunds", "UTM": "UTMSet1", "ProductCode": "ProductCode2", "Pincode": "PincodeSet2"}'

declare @id bigint,@FilterKey varchar(50),@JsonKeyValue varchar(50),@ChildId bigint=0,@CampaignId varchar(50)
select @id = id, @FilterKey=FilterKey from tblLeadSourceMapping where LeadSource = @LeadSource and TokenSource=@TokenSource

if(@FilterKey is not null)
Begin
		SELECT @JsonKeyValue = value FROM OPENJSON(@json_data) WHERE [key] = @FilterKey
		create table #tmp(Id bigint)
		create table #tmp2(Id bigint)
		insert into #tmp
		select Id from tblCampaignMapping where LSMap=@id and Parameter=@FilterKey and Value = @JsonKeyValue 

		while(@id is not null)
		begin
				if((select count(Id) from #tmp) <= 0)
				Begin
					break;
				End
				else if((select count(Id) from tblCampaignMapping where Id in(select Id from #tmp) and CampaignId is not null) > 0)
				Begin
						select @CampaignId = CampaignId from tblCampaignMapping 
						where Id in(select Id from #tmp) and Parameter=@FilterKey and Value = @JsonKeyValue and CampaignId is not null
						break;	
				End	
				else
				Begin	
						insert into #tmp2 select Id from #tmp
						truncate table #tmp
						insert into #tmp
						select ChildId from tblCampaignMapping where Id in(select Id from #tmp2) and Parameter=@FilterKey and Value = @JsonKeyValue
						truncate table #tmp2

						select @FilterKey = Parameter from tblCampaignMapping where Id in(select Id from #tmp)						
						SELECT @JsonKeyValue = value FROM OPENJSON(@json_data) WHERE [key] = @FilterKey		
						select @FilterKey,@JsonKeyValue
						select * from #tmp						
				End				
		end
End
drop table #tmp
drop table #tmp2
select @CampaignId as CampaignId


 */

