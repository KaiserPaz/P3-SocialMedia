using System;
using System.Collections.Generic;

namespace SocialMediaWebAPI.Models.EF
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
            UserFriendFriends = new HashSet<UserFriend>();
            UserFriendUsers = new HashSet<UserFriend>();
        }

        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobilePhone { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? RegisteredAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Intro { get; set; }
        public string? Profile { get; set; }
        public string? ProfilePicture { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserFriend> UserFriendFriends { get; set; }
        public virtual ICollection<UserFriend> UserFriendUsers { get; set; }
    }
}
