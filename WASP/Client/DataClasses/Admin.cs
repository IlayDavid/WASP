namespace Client.DataClasses
{
    public class Admin
    {
        public User user;
        public Admin(User admin)
        {
            this.user = admin;
        }
    }
}