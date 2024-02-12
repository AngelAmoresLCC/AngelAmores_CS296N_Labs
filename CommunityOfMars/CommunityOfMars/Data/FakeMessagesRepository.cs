using CommunityOfMars.Models;

namespace CommunityOfMars.Data
{
    public class FakeMessagesRepository : IMessagesRepository
    {
        List<Message> messages = new();

        public async Task<Message> GetMessageById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Message>> GetMessages()
        {
            throw new NotImplementedException();
        }

        public async Task<int> StoreMessage(Message message)
        {
            message.MessageId = messages.Count + 1;
            messages.Add(message);
            return message.MessageId;
        }
    }
}
