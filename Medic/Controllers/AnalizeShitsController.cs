using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.Controllers
{
    class AnalizeShitsController : AnalizeController
    {
        public List<AnalizeShit> GetAnalizeShits()
        {
            var list = new List<AnalizeShit>();
            MedicModel db = null;

            using (db = new MedicModel())
            {
                try
                {
                    list = db.AnalizeShits.Include(e => e.Analizi.Client).Include(e=> e.Analizi.Laborant).ToList();
                }
                finally
                {
                    db.Dispose();
                };

            }
            return list;
        }

        public void AddAnalizeShit(AnalizeShit AnalizeShit)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.AnalizeShits.Add(AnalizeShit);
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void UpdateAnalizeShit(AnalizeShit AnalizeShit)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(AnalizeShit.Analizi).State = EntityState.Modified;
                    db.Entry(AnalizeShit).State = EntityState.Modified;
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void DeleteAnalizeShit(AnalizeShit AnalizeShit)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(AnalizeShit.Analizi).State = EntityState.Deleted;
                    db.Entry(AnalizeShit).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }
    }
}
