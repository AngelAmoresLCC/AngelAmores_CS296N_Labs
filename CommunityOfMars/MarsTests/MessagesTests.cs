using CommunityOfMars.Controllers;
using CommunityOfMars.Data;
using CommunityOfMars.Models;

namespace MarsTests
{
    public class MessagesTests
    {
        [Fact]
        public void MessagePostTest()
        {
            //Arrange
            var repo = new FakeMessagesRepository();
            HomeController testController = new(repo);
            Message message = new() { Sender = new AppUser { UserName = "TestS" }, Receiver = new AppUser { UserName = "TestR" },
                Title = "Test Message", Body = "This message is for testing purposes.", Priority = 5 };

            //Act
            testController.Message(message);

            //Assert
            Assert.True(message.MessageId > 0);
            Assert.Equal(DateTime.Now.ToShortDateString(), message.Date.ToShortDateString());
        }
    }
}
