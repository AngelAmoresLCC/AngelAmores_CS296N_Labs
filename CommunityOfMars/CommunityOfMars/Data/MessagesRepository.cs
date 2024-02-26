using CommunityOfMars.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityOfMars.Data
{
    public class MessagesRepository : IMessagesRepository
    {
        private MarsDBContext dbContext;
        public MessagesRepository(MarsDBContext context) 
        {
            dbContext = context;
        }

        public async Task<Message> GetMessageById(int id)
        {
            return dbContext.Messages
                .Where(m => m.MessageId == id)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .FirstOrDefault();
        }

        public async Task<List<Message>> GetMessages()
        {
            return dbContext.Messages
                .Where(m => m.Parent == 0)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Include(m => m.Replies)
                .ToList();
        }

        public async Task<int> StoreMessage(Message message)
        {
            dbContext.Messages.Add(message);
            //Returns number of saved objects, should be 3 for now
            return dbContext.SaveChanges();
        }

        public async Task<int> StoreReply(Message newReply)
        {
            dbContext.Messages.Add(newReply);
            Message oldMessage = await GetMessageById(newReply.Parent);
            oldMessage.Replies.Add(newReply);
            dbContext.Messages.Update(oldMessage);
            return dbContext.SaveChanges();
        }

        public int DeleteMessage(int messageId)
        {
            Message? message = dbContext.Messages.Find(messageId);
            if (message is not null)
                dbContext.Remove(message);
            return dbContext.SaveChanges();
        }
    }
}
