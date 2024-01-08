using CommunityOfMars.Data;
using CommunityOfMars.Models;

namespace CommunityOfMars
{
    public class SeedData
    {
        public static void Seed(MarsDBContext context)
        {
            if (!context.Messages.Any())
            {
                Message message = new()
                {
                    Sender = new() { UserName = "System" },
                    Receiver = new() { UserName = "All" },
                    Title = "Welcome!",
                    Body = "Welcome to the message board! Feel free to send messages here!",
                    Priority = 0,
                    Date = DateOnly.Parse("11/11/1111")
                };
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }
    }
}
