using System;
using System.Collections.Generic;

namespace FriendController.Models
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

        public virtual User? Friend { get; set; }
    }
}
