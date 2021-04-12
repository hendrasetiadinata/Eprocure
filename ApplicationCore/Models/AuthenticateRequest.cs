namespace ApplicationCore.Models
{
    public class AuthenticateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public AuthenticateRequest()
        {

        }

        public AuthenticateRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
