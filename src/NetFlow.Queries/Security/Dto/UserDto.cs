using System;
using NetFlow.Infrastructure.Database;

namespace NetFlow.Queries.Security.Dto
{
    public class UserDto : DtoBase<Guid>
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
