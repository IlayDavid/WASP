namespace Client.DataClasses
{
    public class Notification
    {
        public int id { get; set; }
        public string message { get; set; }
        public User source;
        public User target;
    }
}