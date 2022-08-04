using FriendController.Models;
using SocialMediaWebAPI.Models;

namespace SocialMediaWebAPI.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public virtual ICollection<UserFriendDto>? UserFriends { get; set; }


    }
}