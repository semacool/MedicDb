using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.Controllers
{
    class AnalizeController
    {
        public List<Client> getClients() 
        {
            List<Client> list = new List<Client>();
            using(var db = new MedicModel()) 
            {
                list = db.Clients.ToList();
            }
            return list;
        }

        public List<Laborant> getLaborants()
        {
            List<Laborant> list = new List<Laborant>();
            using (var db = new MedicModel())
            {
                list = db.Laborants.ToList();
            }
            return list;
        }
    }
}
