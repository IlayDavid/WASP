namespace Client.DataClasses
{
    public class SuperUser : User
    {
        public SuperUser(string name, string userName, int ID, string email, string pass) : base(ID, name, userName, email, pass)
        {
        }
    }
}