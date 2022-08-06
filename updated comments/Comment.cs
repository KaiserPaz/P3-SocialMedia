using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace SocialMediaWebAPI.Models.EF
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string? Comment1 { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? PostId { get; set; }
        public string? CommentImage { get; set; }

        public virtual Post? Post { get; set; }

        #region SqlConnection
        SqlConnection con = new SqlConnection(@"Server=tcp:nikhils-p2.database.windows.net,1433;Initial Catalog=SocialDB;Persist Security Info=False;User ID=trainer;Password=Password@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        #endregion

        #region UpdateComment
        public string UpdateComment(int p_commentId, string p_comment1)
        {
            SqlCommand cmd = new SqlCommand("update Comment set comment = @p_comment1 and updatedAt = @p_updateAt where commentId = @p_commentId", con);
            cmd.Parameters.AddWithValue("@p_commentId", p_comment1);
            cmd.Parameters.AddWithValue("@p_comment1", p_comment1);
            cmd.Parameters.AddWithValue("@p_updateAt", System.Data.SqlDbType.DateTime);

            con.Open();
            int editComment = cmd.ExecuteNonQuery();
            con.Close();

            if (editComment == 1)
            {
                return "Comment Updated Successfully";
            }
            throw new Exception("Invalid Comment, not found in system");
        }
        #endregion
    }
}
