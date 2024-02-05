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
            return dbContext.Messages.Find(id);
        }

        public async Task<List<Message>> GetMessages()
        {
            return dbContext.Messages
                .Include(message => message.Sender)
                .Include(message => message.Receiver)
                .ToList();
        }

        public async Task<int> StoreMessage(Message message)
        {
            dbContext.Messages.Add(message);
            //Returns number of saved objects, should be 3 for now
            return dbContext.SaveChanges();
        }
    }
}
