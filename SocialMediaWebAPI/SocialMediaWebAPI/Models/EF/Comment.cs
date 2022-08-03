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
    }
}
