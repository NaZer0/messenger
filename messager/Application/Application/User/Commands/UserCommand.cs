using Messenger.Domain;
using Messenger.DataBase;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Messenger.Application.User.Commands
{
    public static class UserCommand
    {
        public static bool CreateUser(Domain.User user)
        {
            //Create new user
            //!!!Проверка на уникальность имени

            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.Users.SingleOrDefault(p => (p.Name == user.Name)) == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public static int? LoginUser(Domain.User user)
        {
            //Login User
            using (ApplicationContext db = new ApplicationContext())
            {
                Domain.User userdb = db.Users.SingleOrDefault(p => (p.Name == user.Name) && (p.Password == user.Password));

                if (userdb != null) 
                {
                    db.Remove(userdb);
                    db.SaveChanges();
                    userdb.session = Guid.NewGuid().GetHashCode();
                    db.Add(userdb);
                    db.SaveChanges();

                    return userdb.session;
                }

                return null;
            }
        }
        
    }
}
