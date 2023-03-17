using Messenger.Domain;
using Messenger.DataBase;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;

namespace Messenger.Application.Messnge.Commands
{
    public class MessangeCommand
    {
        public static bool CreateMessnge(string session, string text)
        {
            //Create new messange

            using (ApplicationContext db = new ApplicationContext())
            {
                Domain.User? user = db.Users.SingleOrDefault(p => (p.session == Int32.Parse(session)));

                if (user != null)
                {
                    Messang messang = new Messang(Guid.NewGuid().GetHashCode(), user.Name, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc), text);
                    db.Messangs.Add(messang);
                    db.SaveChanges();

                    return true;
                }
                else return false;
            }
        }

        
        public static List<Messang> GetMessanges(int number)
        {
            //Get messanges

            List<Messang> messang = new List<Messang>();

            using (ApplicationContext db = new ApplicationContext())
            {
                messang = (from mes in db.Messangs
                          orderby mes.Time
                          select mes).Reverse().Take(number).ToList();

            }
            return messang;
        }

        public static List<Messang> GetMessanges(DateTime time)
        {
            //Get messanges

            List<Messang> messang = new List<Messang>();

            using (ApplicationContext db = new ApplicationContext())
            {
                messang = (from mes in db.Messangs
                           where mes.Time > DateTime.SpecifyKind(time, DateTimeKind.Utc)
                           orderby mes.Time
                           select mes).Reverse().ToList();

            }

            return messang;
        }
    }
}
