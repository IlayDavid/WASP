namespace Client.DataClasses
{
    public class SuperUser : User
    {
        string auth { get; set; }
        public SuperUser() { }
        public SuperUser(string name, string userName, int ID, string email, string pass) : base(ID, name, userName, email, pass)
        {
        }
        public SuperUser(string name, string userName, int ID, string email, string pass, string authen) : base(ID, name, userName, email, pass)
        {
            auth = authen;
        }
    }
}