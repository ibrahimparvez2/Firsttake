using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstTake.Models;

namespace FirstTake.Controllers
{
    public class MatchesController : Controller
    {
        public CoreDataContext db = new CoreDataContext();
        // GET: Matches
        public ActionResult Index()
        {
            MatchesRepository matchesrep = new MatchesRepository();

            var myMatches = matchesrep.GetAllMatches();

            if (myMatches.Count() > 0)
            {
                ViewData["MyMatches"] = myMatches.OrderByDescending(k => k.TimeStamp);
                //viewdata like a tag which contains all the information and put into view
            }

            return View();

        }

        [HttpGet]
        public ActionResult Add()
        {
            List<SelectListItem> teamlist = new List<SelectListItem>();
            TeamsRepository teamsrep = new TeamsRepository();
            var myTeams = teamsrep.GetAllTeams().OrderBy(k => k.TeamName);
            if (myTeams.Count() > 0)
            {

                foreach (Team t in myTeams)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = t.Id.ToString();
                    sli.Text = t.TeamName;
                    teamlist.Add(sli);
                }
                ViewData["Teams"] = teamlist;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Add(Match match)
        {

            List<SelectListItem> teamlist = new List<SelectListItem>();
            TeamsRepository teamsrep = new TeamsRepository();
            var myTeams = teamsrep.GetAllTeams().OrderBy(k => k.TeamName);
            if (myTeams.Count() > 0)
            {

                foreach (Team t in myTeams)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = t.Id.ToString();
                    sli.Text = t.TeamName;
                    teamlist.Add(sli);
                }
                ViewData["Teams"] = teamlist;
                //duplicating the dropdownlist if we make errors....


                if (match.HomeTeamId == match.AwayTeamId)
                {

                    ModelState.AddModelError("HomeTeamId", "Please select different teams");
                    //ModelState.AddModelError("HomeTeamId", "Please select different teams");

                }

                else if (ModelState.IsValid)
                {
                    MatchesRepository matchesrep = new MatchesRepository();
                    {

                        matchesrep.Add(match);
                        matchesrep.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }


            }

            return View(match);
        }
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            List<SelectListItem> teamlist = new List<SelectListItem>();
            TeamsRepository teamsrep = new TeamsRepository();
            var myTeams = teamsrep.GetAllTeams().OrderBy(k => k.TeamName);
            if (myTeams.Count() > 0)
            {

                foreach (Team t in myTeams)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = t.Id.ToString();
                    sli.Text = t.TeamName;
                    teamlist.Add(sli);
                }
                ViewData["Teams"] = teamlist;
            }
                //TeamsRepository teamsrep = new TeamsRepository();
                //Team team = teamsrep.GetTeam(Id);
                Match model = db.Matches.Where(w => w.Id == Id).FirstOrDefault();

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(Match match)
        {

            List<SelectListItem> teamlist = new List<SelectListItem>();
            TeamsRepository teamsrep = new TeamsRepository();
            var myTeams = teamsrep.GetAllTeams().OrderBy(k => k.TeamName);
            if (myTeams.Count() > 0)
            {

                foreach (Team t in myTeams)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = t.Id.ToString();
                    sli.Text = t.TeamName;
                    teamlist.Add(sli);
                }
                ViewData["Teams"] = teamlist;
                //duplicating the dropdownlist if we make errors....


                if (match.HomeTeamId == match.AwayTeamId)
                {

                    ModelState.AddModelError("HomeTeamId", "Please select different teams");
                    //ModelState.AddModelError("HomeTeamId", "Please select different teams");

                }

                else if (ModelState.IsValid)
                {
                    
                    

                        Match model = db.Matches.Where(w => w.Id == match.Id).FirstOrDefault();
                        model.HomeTeamId = match.HomeTeamId;
                        model.AwayTeamId = match.AwayTeamId;
                        model.HomeGoals = match.HomeGoals;
                        model.AwayGoals = match.AwayGoals;
                        model.TimeStamp = match.TimeStamp;
                        db.SubmitChanges();

                        return RedirectToAction("Index");
                    
                }

            }

            return View(match);
        }
    }
}
