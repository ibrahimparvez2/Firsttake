using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTake.Models
{
    public class TeamsRepository
    {
        CoreDataContext db = new CoreDataContext();
        

        public List<Team> GetAllTeams()
        {
            return db.Teams.ToList();
        }


        //public Team GetTeam(int? Id)
        //{
            
           
        //    var teamqry = from team in db.Teams where team.Id == Id select team;
        //    var item = teamqry.FirstOrDefault();
        //    return (item);
        //}

        public void Add(Team t)
        {

            db.Teams.InsertOnSubmit(t);

        }
        public void SaveChanges()
        {
            db.SubmitChanges();
        }
        //public void Update(Team t)
        //{
        //    db.Teams.InsertOnSubmit(t);
        //}
 
    }
}