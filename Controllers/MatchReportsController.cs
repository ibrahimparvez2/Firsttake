using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstTake.Models;

namespace FirstTake.Controllers
{
    public class MatchReportsController : Controller
    {

        public CoreDataContext db = new CoreDataContext();
        // GET: MatchReports
        public ActionResult Index()

        {
            MatchReportsRepository matchreprep = new MatchReportsRepository();
            var myMatchReports = matchreprep.GetAllMatchReports();
            if (myMatchReports.Count() > 0)
            {
                ViewData["Reports"] = myMatchReports.OrderByDescending(k => k.Match1.TimeStamp);
                //viewdata like a tag which contains all the information and put into view
            }

            

            return View();
        }


        [HttpGet]
        public ActionResult Add()
        {
            List<SelectListItem> matchesList = new List<SelectListItem>();

            MatchesRepository matchesRep = new MatchesRepository();
            var myMatches = matchesRep.GetAllMatches().OrderBy(k => k.TimeStamp);
            if (myMatches.Count() > 0)
            {

                foreach (Match m in myMatches)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = m.Id.ToString();
                    sli.Text = m.HomeTeamName + "  ( " + m.HomeGoals + "-" + m.AwayGoals + " )  " + m.AwayTeamName;
                    matchesList.Add(sli);
                }

            }

            ViewData["Matches"] = matchesList;

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Add(MatchReport report)
        {
            List<SelectListItem> matchesList = new List<SelectListItem>();
            MatchesRepository matchesRep = new MatchesRepository();
            var myMatches = matchesRep.GetAllMatches().OrderBy(k => k.TimeStamp);
            if (myMatches.Count() > 0)
            {

                foreach (Match m in myMatches)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = m.Id.ToString();
                    sli.Text = m.HomeTeamName + "  ( " + m.HomeGoals + "-" + m.AwayGoals + " )  " + m.AwayTeamName;
                    matchesList.Add(sli);
                }
                ViewData["Matches"] = matchesList;
                if (ModelState.IsValid)
                {
                    MatchReportsRepository matchreportrep = new MatchReportsRepository();

                    matchreportrep.Add(report);
                    matchreportrep.SaveChanges();

                    return RedirectToAction("Index");
                    

                }

            }

            return View(report);
        }

        public ActionResult Edit(int? Id)
        {

            List<SelectListItem> matchesList = new List<SelectListItem>();

            MatchesRepository matchesRep = new MatchesRepository();
            var myMatches = matchesRep.GetAllMatches().OrderBy(k => k.TimeStamp);
            if (myMatches.Count() > 0)
            {

                foreach (Match m in myMatches)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = m.Id.ToString();
                    sli.Text = m.HomeTeamName + "  ( " + m.HomeGoals + "-" + m.AwayGoals + " )  " + m.AwayTeamName;
                    matchesList.Add(sli);
                }

            }

            ViewData["Matches"] = matchesList;

            MatchReport model = db.MatchReports.Where(l => l.Id == Id).FirstOrDefault();

            return View(model);

        }


        [ValidateInput(false)]
        [HttpPost]

        public ActionResult Edit(MatchReport report)
        {
            List<SelectListItem> matchesList = new List<SelectListItem>();
            MatchesRepository matchesRep = new MatchesRepository();
            var myMatches = matchesRep.GetAllMatches().OrderBy(k => k.TimeStamp);
            if (myMatches.Count() > 0)
            {

                foreach (Match m in myMatches)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = m.Id.ToString();
                    sli.Text = m.HomeTeamName + "  ( " + m.HomeGoals + "-" + m.AwayGoals + " )  " + m.AwayTeamName;
                    matchesList.Add(sli);
                }
                ViewData["Matches"] = matchesList;
                if (ModelState.IsValid)
                {
                    MatchReport model = db.MatchReports.Where(l => l.Id == report.Id).FirstOrDefault();

                    model.Contents = report.Contents;
                    db.SubmitChanges();
                    return RedirectToAction("Index");

                }

            }

            return View(report);
        }

        public ActionResult View(int Id)
        {
            MatchReportsRepository matchesrep = new MatchReportsRepository();
            var report = matchesrep.GetAllMatchReports().Where(l =>l.Id == Id).FirstOrDefault();

            if (report != null)
            {
                return View(report);
            }

            else
            {
                return Redirect("Index");
            }
           
        }

        public ActionResult All()

        {
            MatchReportsRepository matchreprep = new MatchReportsRepository();
            var myMatchReports = matchreprep.GetAllMatchReports();
            if (myMatchReports.Count() > 0)
            {
                ViewData["Reports"] = myMatchReports.OrderByDescending(k => k.Match1.TimeStamp).ThenByDescending(k => k.Id);
                //viewdata like a tag which contains all the information and put into view
            }



            return View();
        }
    }
}