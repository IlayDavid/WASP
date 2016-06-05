namespace Client.DataClasses
{
    public class Policy
    {
        public static int owner = 1;
        public static int moderator = 2;
        public static int admin = 4;
        public static int all = 7;


        //post
        public int deletePost; //done
        //security
        public int passwordPeriod;
        public bool emailVerification; //done
        //moderator
        public int seniority;  
        //stress
        public int usersSameTime;

        public string[] questions;

        public Policy(int deletePost, int passwordPeriod, bool emailVerification, int seniority, int usersSameTime)
        {
            this.deletePost = deletePost;
            this.passwordPeriod = passwordPeriod;
            this.emailVerification = emailVerification;
            this.seniority = seniority;
            this.usersSameTime = usersSameTime;
        }

        public Policy(int deletePost, int passwordPeriod, bool emailVerification, int seniority, int usersSameTime, string[] questions)
        {
            this.deletePost = deletePost;
            this.passwordPeriod = passwordPeriod;
            this.emailVerification = emailVerification;
            this.seniority = seniority;
            this.usersSameTime = usersSameTime;
            this.questions = questions;
        }
        public bool isOwnerCanDeletePost()
        {
            return (deletePost & owner) != 0;
        }
        public bool isModeratorCanDeletePost()
        {
            return (deletePost & moderator) != 0;
        }
        public bool isAdminCanDeletePost()
        {
            return (deletePost & admin) != 0;
        }
    }
}