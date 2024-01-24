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
        public IActionResult Messages(string messageId = "")
        {
            var messages = new List<Message>();
            if (messageId.Length > 0)
            {
                messages.Add(messagesRepo.GetMessageById(int.Parse(messageId)));
            }
            else
            {
                messages = messagesRepo.GetMessages();
            }
            return View(messages);
        }

        [Authorize]
        public IActionResult Message()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Message(Message message)
        {
            message.Date = DateOnly.FromDateTime(DateTime.Now);
            message.Sender = userManager.GetUserAsync(User).Result;
            messagesRepo.StoreMessage(message);
            return RedirectToAction("Messages");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}