using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KeepOnTrack.Models;
using Microsoft.Office365.Exchange;

namespace KeepOnTrack.Controllers {
  public class HomeController : Controller {
    public ActionResult Index() {
      return View();
    }

    public ActionResult About() {
      ViewBag.Message = "Your about page.";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}