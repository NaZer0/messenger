using Messenger.Domain;
using Messenger.DataBase;
using System.Xml.Linq;

namespace Messenger.Application.User.Commands
{
    public static class UserCommand
    {
        public static bool CreateUser(Domain.User user)
        {
            //Create new user

            //Check unique name
            if (DataBase.MsDataBase.UniqueName(user.Name))
            {
                //Other save Data Base
                bool result = DataBase.MsDataBase.CreateUser(user);

                return result;
            }
            else { return false; }
        }
        public static int? LoginUser(Domain.User user)
        {
            //Login User

            int? session = DataBase.MsDataBase.LoginUser(user);

            return session;
        }
        
    }
}
