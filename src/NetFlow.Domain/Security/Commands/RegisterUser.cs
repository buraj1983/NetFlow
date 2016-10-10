using System;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Domain.Security.Commands
{
    public class RegisterUser : Command
    {
        public string Login { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get;}

        public RegisterUser(string login, string password, string firstName, string lastName, string email)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
