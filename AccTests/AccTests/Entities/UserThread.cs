namespace AccTests
{
    public class UserThread
    {
        public UserThread (string subject, Post openPost)
        {
            _subject = subject;
            _openPost = openPost;
        }

        public string _subject { get; set; }
        public Post _openPost { get; set;}
    }
}