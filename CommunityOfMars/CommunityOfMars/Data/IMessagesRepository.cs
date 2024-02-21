using CommunityOfMars.Models;

namespace CommunityOfMars.Data
{
    public interface IMessagesRepository
    {
        public Task<List<Message>> GetMessages();
        public Task<Message> GetMessageById(int id);
        public Task<int> StoreMessage(Message message);
        public int DeleteMessage(int messageId);
    }
}
