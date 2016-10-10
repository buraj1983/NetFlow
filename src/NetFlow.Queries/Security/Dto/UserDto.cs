using System;
using NetFlow.Infrastructure.Queries.Dto;

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
