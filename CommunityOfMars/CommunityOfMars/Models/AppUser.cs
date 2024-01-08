using System.ComponentModel.DataAnnotations;

namespace CommunityOfMars.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public string? UserName { get; set; }
    }
}