using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.Controllers
{
    class AnalizeMochasController : AnalizeController
    {
        public List<AnalizeMocha> GetAnalizeMochas()
        {
            var list = new List<AnalizeMocha>();
            MedicModel db = null;

            using (db = new MedicModel())
            {
                try
                {
                    list = db.AnalizeMochas.Include(e => e.Analizi.Client).Include(e=> e.Analizi.Laborant).ToList();
                }
                finally
                {
                    db.Dispose();
                };

            }
            return list;
        }

        public void AddAnalizeMocha(AnalizeMocha AnalizeMocha)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.AnalizeMochas.Add(AnalizeMocha);
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void UpdateAnalizeMocha(AnalizeMocha AnalizeMocha)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(AnalizeMocha.Analizi).State = EntityState.Modified;
                    db.Entry(AnalizeMocha).State = EntityState.Modified;
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void DeleteAnalizeMocha(AnalizeMocha AnalizeMocha)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(AnalizeMocha.Analizi).State = EntityState.Deleted;
                    db.Entry(AnalizeMocha).State = EntityState.Deleted;
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
