using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KeepOnTrack.Models;

namespace KeepOnTrack.Controllers {
  public class CalendarController : Controller {
    [Authorize]
    public async Task<ActionResult> Index() {
      EventRepository repo = new EventRepository();
      
      var events = await repo.GetCalendarEvents();

      return View(events);
    }

    [Authorize]
    public ActionResult Create() {
      return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(EventModel model) {
      EventRepository repo = new EventRepository();

      await repo.CreateCalendarEvent(model);

      return RedirectToAction("Index");
    }
  }
}