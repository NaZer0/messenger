using Messenger.Domain;
using Messenger.DataBase;
using System.Reflection;
using System.Xml.Linq;

namespace Messenger.Application.Messnge.Commands
{
    public class MessangeCommand
    {
        public static bool CreateMessnge(string session, string text)
        {
            //Create new messange
            //Get User
            string? name = DataBase.MsDataBase.GetUserName(session);

            //Create messange
            Messang mess = new Messang(name, DateTime.Now, text);

            //Other save Data Base
            bool result = DataBase.MsDataBase.CreateNewMessage(mess);

            return result;
        }

        
        public static List<Messang> GetMessanges(int number)
        {
            //Get messanges

            //Other save Data Base
            List <Messang> messangs = DataBase.MsDataBase.GetMessage(number);

            return messangs;
        }

        public static List<Messang> GetMessanges(DateTime time)
        {
            //Get messanges

            //Other save Data Base
            List<Messang> messangs = DataBase.MsDataBase.GetMessage(time);

            return messangs;
        }
    }
}
