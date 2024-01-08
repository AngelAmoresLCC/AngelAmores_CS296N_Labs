using System.Xml;

namespace CommunityOfMars.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public AppUser? Sender { get; set; }
        public AppUser? Receiver { get; set; }
        public int Priority { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public DateOnly Date { get; set; }
    }
}
