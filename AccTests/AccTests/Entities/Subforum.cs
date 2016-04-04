namespace AccTests
{
    public class Subforum
    {
        public Subforum(string subject, User moderator)
        {
            _subject = subject;
            _moderator = moderator;
        }
        public string _subject { get; set; }
        public User _moderator { get; set; }

    }
}