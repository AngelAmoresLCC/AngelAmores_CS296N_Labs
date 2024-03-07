using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace CommunityOfMars.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public AppUser? Sender { get; set; }
        public AppUser? Receiver { get; set; }
        public int Priority { get; set; }
        [Required(ErrorMessage = "Message must have a Title.")]
        public string Title { get; set; } = null!;
        public string? Body { get; set; }
        public DateOnly Date { get; set; }
        //public Message? Reply { get; set; }
        [ForeignKey("Parent")]
        public List<Message> Replies { get; set; } = new List<Message>();
        public int? Parent { get; set; }
        public Message? ParentMessage { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
