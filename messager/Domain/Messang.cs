using System.ComponentModel.DataAnnotations;
namespace Messenger.Domain
{
    public class Messang
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [MaxLength(512)]
        public string Text { get; set; }

        public Messang(int id, string name, DateTime time, string text)
        {
            Id = id;
            Name = name;
            Time = time;
            Text = text;
        }
    }
}