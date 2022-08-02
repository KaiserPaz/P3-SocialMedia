using System;
using System.Collections.Generic;

namespace SocialMediaWebAPI.Models.EF
{
    public partial class Post
    {
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
    }
}
