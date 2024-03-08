using CommunityOfMars.Models;

namespace CommunityOfMars.Data
{
    public class FakeMessagesRepository : IMessagesRepository
    {
        List<Message> messages = new();

        public async Task<Message> GetMessageById(int id)
        {
            return messages[id - 1];
        }

        public async Task<List<Message>> GetMessages()
        {
            return messages;
        }

        public async Task<int> StoreMessage(Message message)
        {
            message.MessageId = messages.Count + 1;
            messages.Add(message);
            return message.MessageId;
        }

        public async Task<int> StoreReply(Message newReply)
        {
            throw new NotImplementedException();
        }

        public int DeleteMessage(int messageId)
        {
            try
            {
                messages.RemoveAt(messageId - 1);
            }
            catch { Console.WriteLine("Message at specified index does not exist"); }
            return messages.Count;
        }
    }
}
