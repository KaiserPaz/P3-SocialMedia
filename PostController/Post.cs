using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace SocialMediaWebAPI.Models.EF
{
    public class Post
    {
        #region Parameters created by Scaffold of DB - no changes made to this
        public Post()
        {
            Comments = new HashSet<Comment>();
        }
                
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Image { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        #endregion

        #region SqlConnection
        SqlConnection con = new SqlConnection(@"Server=tcp:nikhils-p2.database.windows.net,1433;Initial Catalog=SocialDB;Persist Security Info=False;User ID=trainer;Password=Password@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        #endregion

        //needs form in UI to only show input for message and image.
        //UserID should be gathered from login, date/time should automatically pull for CreatedAt and UpdatedAt
        #region AddNewPost
        public string AddNewPost(Post newPost)
        {
            SqlCommand cmd = new SqlCommand("insert into Post (userId, message, createdAt, updatedAt, image) values (@p_userId,@p_message,@p_createdAt,@p_updatedAt,@p_image)", con);
            cmd.Parameters.AddWithValue("@p_userId", newPost.UserId);
            cmd.Parameters.AddWithValue("@p_message", newPost.Message);
            cmd.Parameters.AddWithValue("@p_createdAt", newPost.CreatedAt);
            cmd.Parameters.AddWithValue("@p_updatedAt", newPost.UpdatedAt);
            cmd.Parameters.AddWithValue("@p_image", newPost.Image);

            con.Open();
            int recordsAffected = cmd.ExecuteNonQuery();
            con.Close();
            if (recordsAffected == 1)
            {
                return "Post Created Successfully";
            }
            throw new Exception("Post Not Created");
        }
        #endregion

        //needs security to only allow logged in user to update a post *they* created (based on UserID/userName - need method to get userID based on logged in userName)
        //might also need an edit to allow for image to be updated - not included right now
        //Solved:
        //need some way to keep all other fields as they were versus updating to null. I.e. if had picture - reset the image field to same data - resolved
        #region UpdatePost
        public string UpdatePost(int p_Id, string p_Message)
        {
            SqlCommand cmd = new SqlCommand("update Post set message = @p_Message and updatedAt = @p_updateAt where id = @p_Id", con);
            cmd.Parameters.AddWithValue("@p_Id", p_Id);
            cmd.Parameters.AddWithValue("@p_Message", p_Message);
            cmd.Parameters.AddWithValue("@p_updateAt", System.Data.SqlDbType.DateTime);

            con.Open();
            int editMessage = cmd.ExecuteNonQuery();
            con.Close();

            if (editMessage == 1)
            {
                return "Post Updated Successfully";
            }
            throw new Exception("Invalid Post, not found in system");
        }
        #endregion

        #region GetPostByUserId
        public List<Post> GetPostByUserId(int p_userId)
        {
            SqlCommand cmd = new SqlCommand("select * from Post where userId=@p_userId", con);
            cmd.Parameters.AddWithValue("@p_userId", p_userId);

            con.Open();
            List<Post> postByuserId = new List<Post>();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                postByuserId.Add(new Post()
                {   
                    Id = (int)rd[0],
                    UserId = (int)rd[1],
                    Message = rd[2].ToString(),
                    CreatedAt = (DateTime)rd[3],
                    UpdatedAt = (DateTime)rd[4],
                    Image = rd[5].ToString()
                });
            }
            rd.Close();
            con.Close();

            return postByuserId;
        }
        #endregion 

        //Not used - followed example created by app in the controller to handle this, but works pretty much the same way
        //#region CheckUserIdExist
        //public bool CheckUserIdExist(int p_userId)
        //{
        //    SqlCommand cmd = new SqlCommand("select count(*) from Posts where userId = @p_UserId", con);
        //    cmd.Parameters.AddWithValue("@p_UserId", p_userId);

        //    con.Open();
        //    int count = (int)cmd.ExecuteScalar();
        //    con.Close();
        //    if (count > 1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //#endregion
    }
}
