namespace AccTests
{
    public class Message
    {

        public Message(string subject, string content)
        {
            _subject = subject;
            _content = content;
        }

        public string _subject { set; get; }
        public string _content { set; get; }
    }
}