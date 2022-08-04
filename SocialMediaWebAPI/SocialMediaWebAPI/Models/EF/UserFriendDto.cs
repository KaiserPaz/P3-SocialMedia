namespace FriendController.Models
{
    public class UserFriendDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FriendId { get; set; }
        public string? FriendStatus { get; set; }
        public string? User { get; set; }
        public string? Friend { get; set; }

    }
}