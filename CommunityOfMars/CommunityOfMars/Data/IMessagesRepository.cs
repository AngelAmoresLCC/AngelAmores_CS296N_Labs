using CommunityOfMars.Models;

namespace CommunityOfMars.Data
{
    public interface IMessagesRepository
    {
        public List<Message> GetMessages();
        public Message GetMessageById(int id);
        public int StoreMessage(Message message);
    }
}
