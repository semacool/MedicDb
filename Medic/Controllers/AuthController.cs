using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.Controllers
{
    class AuthController
    {
        /// <summary>
        /// Метод для входа
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Id Роли аккаунта (-1 - Пользователь не найден)</returns>
        public int Auth(string login, string password)
        {
            int id = -1;
            using(var db = new MedicModel()) 
            {
                var task = db.Users.FirstOrDefault(e => e.Login == login && e.Password == password);
                var user = task;
                if (user != null)
                    id = user.IdType;
            }
            if(id==-1) throw new Exception("Неверный логин или пароль");
            return id;
        }
    }
}
