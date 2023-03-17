using System.ComponentModel.DataAnnotations;

namespace Messenger.Domain
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        public int? session { get; set;}
    
        public User (int id ,string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }
}
