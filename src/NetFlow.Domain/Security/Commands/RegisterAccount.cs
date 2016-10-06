using System;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Domain.Security.Commands
{
    public class RegisterAccount : Command
    {
        public string Username { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; set; }

        public RegisterAccount(string username, string password, string firstName, string lastName, string email)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
