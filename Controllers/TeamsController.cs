using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstTake.Models; 
namespace FirstTake.Controllers
{
    public class TeamsController : Controller
    {
        
        public CoreDataContext db = new CoreDataContext();

        public ActionResult Index()

        {
            //new instance of data rep layer 
            TeamsRepository teamsrep = new TeamsRepository();

            var myTeams = teamsrep.GetAllTeams();
            if (myTeams.Count() > 0)
            {
                ViewData["MyTeams"] = myTeams.OrderBy(x => x.TeamName);
                //viewdata like a tag which contains all the information and put into view
            }

            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Add(Team team)
        {
            if (!String.IsNullOrWhiteSpace(team.TeamName))
            {
                TeamsRepository teamsrep = new TeamsRepository();

                if (teamsrep.GetAllTeams().Where(k => k.TeamName.ToLower() == team.TeamName.ToLower()).Count() > 0)
                {
                    //team name is already in use
                    ModelState.AddModelError("TeamName", "that team is already in use");
                }

                else
                {
                    teamsrep.Add(team);
                    teamsrep.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View(team);

        }

        public ActionResult Edit(int? Id)
        {

            //TeamsRepository teamsrep = new TeamsRepository();
            //Team team = teamsrep.GetTeam(Id);
            Team model = db.Teams.Where(w => w.Id == Id).FirstOrDefault();
        
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Team team)
        {
            if (!String.IsNullOrWhiteSpace(team.TeamName))
            {
                TeamsRepository teamsrep = new TeamsRepository();
                
                if (teamsrep.GetAllTeams().Where(k => k.TeamName.ToLower() == team.TeamName.ToLower()).Count() > 0)
                {
                    //team name is already in use
                    ModelState.AddModelError("TeamName", "that team is already in use");
                }
                else
                {
                    Team model = db.Teams.Where(w => w.Id == team.Id).FirstOrDefault();
                    model.TeamName = team.TeamName;
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }


            }
            return View(team);
        }

        public ActionResult All()

        {
            //new instance of data access layer 
            TeamsRepository teamsrep = new TeamsRepository();

            var myTeams = teamsrep.GetAllTeams();
            if (myTeams.Count() > 0)
            {
                ViewData["Teams"] = myTeams.OrderBy(x => x.TeamName);
                //viewdata like a tag which contains all the information and put into view
            }

            return View();
        }

        public ActionResult View(int Id)
        {
            TeamsRepository teamssrep = new TeamsRepository();

            var allteams = teamssrep.GetAllTeams().Where(l => l.Id != Id).OrderBy(l => l.TeamName);

            
            if (allteams != null)
            {
                ViewData["Teams"] = allteams;
            }


            var team = teamssrep.GetAllTeams().Where(l => l.Id == Id).FirstOrDefault();

            if (team != null)


            {

                MatchReportsRepository matchesreportrep = new MatchReportsRepository();

                var allreports = matchesreportrep.GetAllMatchReports().Where(l => l.Match1.HomeTeamId == Id || l.Match1.AwayTeamId == Id).OrderByDescending(l => l.Match1.TimeStamp);

                if (Request.QueryString["Opponent"] != null)

                {

                    var opponent = allteams.Where(l => l.Id.ToString() == Request.QueryString["Opponent"].ToString()).FirstOrDefault();
                    ViewData["OpponentTeamName"] = opponent.TeamName;
                    allreports = allreports.Where(l => l.Match1.HomeTeamId == opponent.Id || l.Match1.AwayTeamId == opponent.Id).OrderByDescending(l => l.Match1.TimeStamp);
                }


                var allhomegames = allreports.Where(l => l.Match1.HomeTeamId == Id);
                var allawaygames = allreports.Where(l => l.Match1.AwayTeamId == Id);

                if (allhomegames.Count() > 0)
                {

                    ViewData["HomeGames"] = allhomegames;
                }
                if (allawaygames.Count() > 0)
                {

                    ViewData["AwayGames"] = allawaygames;
                }

                return View(team);
            }

            else
            {
                return Redirect("Index");
            }

        }
    }
}