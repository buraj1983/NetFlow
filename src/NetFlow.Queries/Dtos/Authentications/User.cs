using System;

namespace NetFlow.Queries.Dtos.Authentications
{
    public class User : Dto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
