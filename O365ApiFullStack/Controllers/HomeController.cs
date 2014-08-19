using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using O365ApiFullStack.Models;
using Microsoft.Office365.OAuth;

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
            try
            { 
                var contacts = await ContactsAPISample.GetContacts();
                return View(contacts);
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
        }

        [HttpGet]
        public ActionResult ComposeMessage(string recipient)
        {
            try
            { 
                EmailModel newModel = new EmailModel(){ Recipient = recipient};
                return View(newModel);
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult> ComposeMessage(EmailModel model)
        {
            try
            { 
                await MailApiSample.SendMessage(model);
                return RedirectToAction("Index");
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
        }

        public async Task<ActionResult> Mail()
        {
            try
            { 
                var mails = await MailApiSample.GetMessages();
                return View(mails);
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
        }


        public async Task<ActionResult> Events()
        {
            try
            { 
                var events = await CalendarAPISample.GetCalendarEvents();
                return View(events);            
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
        }

        public async Task<ActionResult> Users()
        {
            try
            {
                var users = await ActiveDirectoryApiSample.GetUsers();
                return View(users);
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
        }


        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
        }

        public ActionResult Contact()
        {
            try
            { 
                ViewBag.Message = "Your contact page.";

                return View();
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
        }
    }
}