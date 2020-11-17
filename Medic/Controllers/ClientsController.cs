using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Medic.Controllers
{
    class ClientsController
    {
        public List<Client> GetClients()
        {
            var list = new List<Client>();
            MedicModel db = null;

            using (db = new MedicModel())
            {
                try
                {
                    list = db.Clients.Include(e => e.User).ToList();
                }
                finally
                {
                    db.Dispose();
                };

            }
            return list;
        }


        public void AddClient(Client client)
        {
            using (var db = new MedicModel())
            {
                try
                {
                     db.Clients.Add(client);
                     db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void UpdateClient(Client client)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(client.User).State = EntityState.Modified;
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        public void DeleteClient(Client client)
        {
            using (var db = new MedicModel())
            {
                try
                {
                    db.Entry(client.User).State = EntityState.Deleted;
                    db.Entry(client).State = EntityState.Deleted;
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
