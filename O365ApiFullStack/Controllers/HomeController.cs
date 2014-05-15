using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using O365ApiFullStack.Models;

namespace O365ApiFullStack.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Contacts()
        {
            var contacts = await ContactsAPISample.GetContacts();
            return View(contacts);
        }

        [HttpGet]
        public ActionResult ComposeMessage(string recipient)
        {
            EmailModel newModel = new EmailModel(){ Recipient = recipient};
            return View(newModel);
        }

        [HttpPost]
        public async Task<ActionResult> ComposeMessage(EmailModel model)
        {
            await MailApiSample.SendMessage(model);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Mail()
        {
            var mails = await MailApiSample.GetMessages();
            return View(mails);
        }


        public async Task<ActionResult> Events()
        {
            var events = await CalendarAPISample.GetCalendarEvents();
            return View(events);
        }

        public async Task<ActionResult> Users()
        {
            var users = await ActiveDirectoryApiSample.GetUsers();
            return View(users);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}