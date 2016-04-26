namespace Client.DataClasses
{
    public class SuperUser : User
    {
        public SuperUser(string name, string userName, int ID, string email, string pass)
        {
            this.name = name;
            this.userName = userName;
            this.id = ID;
            this.email = email;
            this.password = pass;
        }
    }
}