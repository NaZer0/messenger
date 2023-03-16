namespace Messenger.Domain
{
    public class User
    {
        public string Name {get;set;}
        public string Password {get;set;}
        public int? session { get;set;}
    
        public User (string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
