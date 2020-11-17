using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.Controllers
{
    class LaborantsController
    {
        public List<Laborant> GetLaborants()
        {
            var list = new List<Laborant>();
            MedicModel db = null;

            using (db = new MedicModel())
            {
                try
                {
                    list = db.Laborants
                        .Include(e => e.User)
                        .Include(e=>e.Laboratory).ToList();
                }
                finally
                {
                    db.Dispose();
                };

            }
            return list;
        }

        public List<Laboratory> GetLaboratories()
        {
            var list = new List<Laboratory>();
            MedicModel db = null;

            using (db = new MedicModel())
            {
                try
                {
                    list = db.Laboratories.ToList();
                }
                finally
                {
                    db.Dispose();
                };

            }
            return list;
        }

        public void AddLaborant(Laborant Laborant)
        {
            using (var db = new MedicModel())
            {
                try
                {
                     db.Laborants.Add(Laborant);
                     db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void UpdateLaborant(Laborant Laborant)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(Laborant.User).State = EntityState.Modified;
                    db.Entry(Laborant).State = EntityState.Modified;
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void DeleteLaborant(Laborant Laborant)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(Laborant.User).State = EntityState.Deleted;
                    db.Entry(Laborant).State = EntityState.Deleted;
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
