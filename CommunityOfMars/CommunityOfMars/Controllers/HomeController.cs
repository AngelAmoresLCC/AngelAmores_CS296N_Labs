using CommunityOfMars.Data;
using CommunityOfMars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CommunityOfMars.Controllers
{
    public class HomeController : Controller
    {
        //MarsDBContext context;
        private IMessagesRepository messagesRepo;
        private UserManager<AppUser> userManager;

        public HomeController(IMessagesRepository m, UserManager<AppUser> userMngr)
        {
            //context = c;
            messagesRepo = m;
            userManager = userMngr;
        }

        public IActionResult Index(string messageId)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        //TODO: Do something with the messageId
        public async Task<IActionResult> Messages(string messageId = "")
        {
            var messages = new List<Message>();
            if (messageId.Length > 0)
            {
                messages.Add(await messagesRepo.GetMessageById(int.Parse(messageId)));
            }
            else
            {
                messages = await messagesRepo.GetMessages();
            }
            return View(messages);
        }

        [Authorize]
        public IActionResult Message()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Message(Message message)
        {
            message.Date = DateOnly.FromDateTime(DateTime.Now);
            message.Sender = await userManager.GetUserAsync(User);
            try
            {
                message.Receiver = await userManager.FindByNameAsync(message.Receiver.UserName);
            } catch { }
			await messagesRepo.StoreMessage(message);
            return RedirectToAction("Messages");
        }

        [Authorize]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            //TODO: Something if the delete fails
            messagesRepo.DeleteMessage(messageId);
            return RedirectToAction("Messages");
        }

        [Authorize]
        public async Task<IActionResult> Reply(int parentId)
        {
            Message parentMessage = await messagesRepo.GetMessageById(parentId);
            return View(parentMessage);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Reply(Message newReply)
        {
            newReply.Date = DateOnly.FromDateTime(DateTime.Now);
            newReply.Sender = await userManager.GetUserAsync(User);
            newReply.Receiver = await userManager.FindByNameAsync(newReply.Receiver.UserName);
            await messagesRepo.StoreReply(newReply);
            return RedirectToAction("Messages");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}