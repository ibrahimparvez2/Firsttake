using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstTake.Models;

namespace FirstTake.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()

        {
            MatchReportsRepository matchreportrep = new MatchReportsRepository();
            var report = matchreportrep.GetAllMatchReports().OrderByDescending(l => l.Match1.TimeStamp).FirstOrDefault();

            return View(report);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}