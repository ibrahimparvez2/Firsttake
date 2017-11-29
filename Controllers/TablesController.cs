using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstTake.Models;

namespace FirstTake.Controllers
{
    public class TablesController : Controller
    {
        // GET: Tables
        public ActionResult Index()

        {
            List<LeagueTableResult> results = new List<LeagueTableResult>();
            MatchesRepository matchesrep = new MatchesRepository();
            TeamsRepository teamsrep = new TeamsRepository();

            var allteams = teamsrep.GetAllTeams();
            var allmatches = matchesrep.GetAllMatches();
            if (allteams.Count() > 0)
            {
                foreach(Team t in allteams)
                {

                    LeagueTableResult res = new LeagueTableResult();
                    res.TeamId = t.Id;
                    res.TeamName = t.TeamName;

                    //calc number of wins for team
                    int wins = 0;
                    int losses = 0;
                    int draws = 0;
                    int goalsfor = 0;
                    int goalsagainst = 0;
                    int points = 0;

                    var allteammatches = allmatches.Where(l => l.HomeTeamId == t.Id || l.AwayTeamId == t.Id);
                    var allteamhomematches = allteammatches.Where(l => l.HomeTeamId == t.Id);
                    var allteamawaymatches = allteammatches.Where(l => l.AwayTeamId == t.Id);

                    foreach(Match m in allteamhomematches)
                    {

                        if (m.HomeGoals > m.AwayGoals)
                        {
                            wins++;
                            points += 3;

                        }
                        if (m.HomeGoals == m.AwayGoals)
                        {
                            draws++;
                            points += 1;
                        }

                        if(m.HomeGoals < m.AwayGoals)
                        {
                            losses++;
                        }

                        goalsfor += (int) m.HomeGoals;
                        goalsagainst += (int)m.AwayGoals;

                    }

                    foreach(Match m in allteamawaymatches)
                    {
                        if (m.AwayGoals > m.HomeGoals)
                        {
                            wins++;
                            points += 3;
                        }
                        if (m.AwayGoals == m.HomeGoals)
                        {
                            draws++;
                            points += 1;
                        }
                        if (m.AwayGoals < m.HomeGoals)
                        {
                            losses++;
                        }

                        goalsfor += (int) m.AwayGoals;
                        goalsagainst += (int) m.HomeGoals;
                    }
                    res.GoalsFor = goalsfor;
                    res.GoalsAgainst = goalsagainst;
                    res.GoalDifference = goalsfor - goalsagainst;
                    res.Wins = wins;
                    res.Losses = losses;
                    res.Draws = draws;
                    res.Points = points;

                    results.Add(res);
                }

            }

            ViewData["results"] = results.OrderByDescending(k=>k.Points).ThenByDescending(k=>k.GoalDifference);
            

            return View();
        }
    }
}