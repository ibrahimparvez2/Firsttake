using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTake.Models
{
    public class MatchesRepository
    {
        
        CoreDataContext db = new CoreDataContext();

        public List<Match> GetAllMatches()
        {
            return db.Matches.ToList();
        }

        public void Add(Match m)
        {

            db.Matches.InsertOnSubmit(m);

        }
        public void SaveChanges()
        {
            db.SubmitChanges();
        }

    }
}