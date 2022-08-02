using System;
using System.Collections.Generic;

namespace SocialMediaWebAPI.Models.EF
{
    public partial class UserFriend
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FriendId { get; set; }
        public string? FriendStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
