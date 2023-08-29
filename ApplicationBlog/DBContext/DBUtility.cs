using ApplicationBlog.Model;
using ApplicationBlog.Utility;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;
using static ApplicationBlog.Utility.ValidationMessages;

namespace ApplicationBlog.DBContext
{
    public class DBUtility
    {
        private IConfiguration _config;
        private readonly BlogDbContext _blogDB;
        public DBUtility(IConfiguration config, BlogDbContext blogDB)
        {
            _config = config;
            _blogDB = blogDB;
        }

        public List<GetPostCommentsList> GetPostComments(long UserPostId, long UserId)
        {
            List<GetPostCommentsList> lstPostComments = new List<GetPostCommentsList>();
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(_config["ConnectionStrings:ApplicationBlog"].ToString());
            using (SqlCommand cmd = new SqlCommand(ValidationMessages.Procedure.usp_GetPostComments, conn))
            {
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserPostId", UserPostId);                    

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "Data");

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            GetPostCommentsList objPostComments = new GetPostCommentsList()
                            {
                                UserPostCommentId = Convert.ToInt64(dr["UserPostCommentId"]),
                                UserId = Convert.ToInt64(dr["UserId"]),
                                FirstName = Convert.ToString(dr["Firstname"]),
                                LastName = Convert.ToString(dr["Lastname"]),
                                ProfilePic = Convert.ToString(dr["ProfilePic"]),
                                CommentText = Convert.ToString(dr["CommentText"]),
                                IsCurrentUser = Convert.ToInt64(dr["UserId"]) == UserId ? true : false
                            };
                            lstPostComments.Add(objPostComments);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (cmd != null)
                    {
                        if (cmd.Connection.State == ConnectionState.Open || cmd.Connection.State == ConnectionState.Executing ||
                            cmd.Connection.State == ConnectionState.Fetching)
                            cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }
                }
            }
            return lstPostComments;
        }

        public List<GetPostLikeList> SubmitPostLike(long UserId, long UserPostId)
        {
            List<GetPostLikeList> lstPostLike = new List<GetPostLikeList>();
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(_config["ConnectionStrings:ApplicationBlog"].ToString());
            using (SqlCommand cmd = new SqlCommand(ValidationMessages.Procedure.usp_SubmitPostLike, conn))
            {
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserPostId", UserPostId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "Data");

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            GetPostLikeList objPostLike = new GetPostLikeList()
                            {
                                UserId = Convert.ToInt64(dr["UserId"]),
                                FirstName = Convert.ToString(dr["Firstname"]),
                                LastName = Convert.ToString(dr["Lastname"]),
                                ProfilePic = Convert.ToString(dr["ProfilePic"])
                            };
                            lstPostLike.Add(objPostLike);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (cmd != null)
                    {
                        if (cmd.Connection.State == ConnectionState.Open || cmd.Connection.State == ConnectionState.Executing ||
                            cmd.Connection.State == ConnectionState.Fetching)
                            cmd.Connection.Close();
                        cmd.Connection.Dispose();
                    }
                }
            }
            return lstPostLike;
        }
    }
}
