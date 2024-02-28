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
			List<Message> ParentMessages = await dbContext.Messages
				.Where(m => m.Parent == 0)
				.Include(m => m.Sender)
				.Include(m => m.Receiver)
				.Include(m => m.Replies)
				.ThenInclude(r => r.Sender)
				.Include(m => m.Replies)
				.ThenInclude(r => r.Receiver)
				.Include(m => m.Replies)
				.ThenInclude(r => r.Replies)
				.ToListAsync();
			foreach (var m in ParentMessages)
			{
				if (m.Replies.Any())
					foreach (var reply in m.Replies)
					{
						if (reply.Replies.Any())
						{
							reply.Replies = await dbContext.Messages
								.Where(m => m.Parent == reply.MessageId)
								.Include(m => m.Sender)
								.Include(m => m.Receiver)
								.Include(m => m.Replies)
								.ThenInclude(r => r.Sender)
								.Include(m => m.Replies)
								.ThenInclude(r => r.Receiver)
								.Include(m => m.Replies)
								.ThenInclude(r => r.Replies)
								.ToListAsync();
							foreach (var reply2 in reply.Replies)
							{
								if (reply2.Replies.Any())
									reply2.Replies = await dbContext.Messages
										.Where(m => m.Parent == reply2.MessageId)
										.Include(m => m.Sender)
										.Include(m => m.Receiver)
										.Include(m => m.Replies)
										.ThenInclude(r => r.Sender)
										.Include(m => m.Replies)
										.ThenInclude(r => r.Receiver)
										.Include(m => m.Replies)
										.ThenInclude(r => r.Replies)
										.ToListAsync();
							}
						}
					}
			}
			return ParentMessages;
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
