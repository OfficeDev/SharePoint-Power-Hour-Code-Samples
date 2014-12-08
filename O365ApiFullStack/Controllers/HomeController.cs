using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using O365ApiFullStack.Models;

namespace O365ApiFullStack.Controllers {
  public class HomeController : Controller {
    public ActionResult Index() {
      return View();
    }


    [Authorize]
    public async Task<ActionResult> Contacts() {
      ContactRepository repo = new ContactRepository();
      var contacts = await repo.GetContacts();
      return View(contacts);
    }

    [Authorize]
    [HttpGet]
    public ActionResult ComposeMessage(string recipient) {
      EmailModel newModel = new EmailModel() { Recipient = recipient };
      return View(newModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> ComposeMessage(EmailModel model) {
      MailRepository repo = new MailRepository();
      await repo.SendMessage(model);
      return RedirectToAction("Index");
    }

    [Authorize]
    public async Task<ActionResult> Mail() {
      MailRepository repo = new MailRepository();
      var mails = await repo.GetMessages();
      return View(mails);
    }


    [Authorize]
    public async Task<ActionResult> Events() {
      CalendarRepository repo = new CalendarRepository();
      var events = await repo.GetCalendarEvents();
      return View(events);
    }

    [Authorize]
    public async Task<ActionResult> Users() {
      AzureAdRepository repo = new AzureAdRepository();
      var users = await repo.GetUsers();
      return View(users);
    }


    public ActionResult About() {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}