using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KeepOnTrack.Models;
using Microsoft.Office365.Exchange;

namespace KeepOnTrack.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var events = await CalendarAPISample.GetCalendarEvents();
            return View(events);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventModel model)
        {
            await CalendarAPISample.CreateCalendarEvent(model);
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}