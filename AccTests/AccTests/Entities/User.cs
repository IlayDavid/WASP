namespace AccTests
{
    public class User
    {

        public User(string userName, string password, string name, string email)
        {
            _userName = userName;
            _password= password;
            _name = name;
            _email = email
        }

        public string _userName { get; set; }
        public string _name { get; set; }
        public string _password { get; set; }
        public string _email { get; set; }

    }
}