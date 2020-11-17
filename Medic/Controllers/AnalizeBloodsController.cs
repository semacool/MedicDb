using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.Controllers
{
    class AnalizeBloodsController : AnalizeController
    {
        public List<AnalizeBlood> GetAnalizeBloods()
        {
            var list = new List<AnalizeBlood>();
            MedicModel db = null;

            using (db = new MedicModel())
            {
                try
                {
                    list = db.AnalizeBloods.Include(e => e.Analizi.Client).Include(e=> e.Analizi.Laborant).ToList();
                }
                finally
                {
                    db.Dispose();
                };

            }
            return list;
        }

        public void AddAnalizeBlood(AnalizeBlood AnalizeBlood)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.AnalizeBloods.Add(AnalizeBlood);
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void UpdateAnalizeBlood(AnalizeBlood AnalizeBlood)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(AnalizeBlood.Analizi).State = EntityState.Modified;
                    db.Entry(AnalizeBlood).State = EntityState.Modified;
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void DeleteAnalizeBlood(AnalizeBlood AnalizeBlood)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(AnalizeBlood.Analizi).State = EntityState.Deleted;
                    db.Entry(AnalizeBlood).State = EntityState.Deleted;
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
