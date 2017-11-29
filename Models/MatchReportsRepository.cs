using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstTake.Models
{
    public class MatchReportsRepository
    {
        CoreDataContext db = new CoreDataContext();

        public List<MatchReport> GetAllMatchReports()
        {
            return db.MatchReports.ToList();
        }

        public void Add(MatchReport r)
        {

            db.MatchReports.InsertOnSubmit(r);

        }
        public void SaveChanges()
        {
            db.SubmitChanges();
        }

    }
}