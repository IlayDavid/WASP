namespace WASP.DataClasses
{
    public class Message
    {
        private static int _idCounter = 0;
        public int Id { get; set; }
        private string _content;
        private string _title;

        public Message(string content, string title)
        {
            Id = _idCounter++;
            _content = content;
            _title = title;
        }
        public static bool isValid(string content, string title)
        {
            return !(Helper.isEmptyString(content) || Helper.isEmptyString(title));
        }
        internal bool isEmpty()
        {
            return (_content.Equals("") || _title.Equals(""));
        }
    }
}
