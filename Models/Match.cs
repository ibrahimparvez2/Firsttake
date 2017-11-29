using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTake.Models
{
    public partial class Match
    {
        TeamsRepository teamsrep = new TeamsRepository();
        public string HomeTeamName
        {
            get
            {
                return teamsrep.GetAllTeams().Where(l => l.Id == this.HomeTeamId).FirstOrDefault().TeamName;
            }
        }
        public string AwayTeamName
        {
            get
            {
                return teamsrep.GetAllTeams().Where(l => l.Id == this.AwayTeamId).FirstOrDefault().TeamName;
            }
        }

    }
}