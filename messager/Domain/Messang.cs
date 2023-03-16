namespace Messenger.Domain
{
    public class Messang
    {
        public string Name { get; set; }
        public DateTime time { get; set; }
        public string Text { get; set; }

        public Messang(string name, DateTime time, string text)
        {
            Name = name;
            this.time = time;
            Text = text;
        }
    }
}