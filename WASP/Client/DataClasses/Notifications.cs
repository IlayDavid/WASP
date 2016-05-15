namespace Client.DataClasses
{
    public class Notifications
    {
        public int id { get; set; }
        public string message { get; set; }
        public User source;
        public User target;
    }
}