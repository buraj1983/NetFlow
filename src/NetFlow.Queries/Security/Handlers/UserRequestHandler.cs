using System;
using System.Linq;
using NetFlow.Infrastructure.Database;
using NetFlow.Infrastructure.Queries;
using NetFlow.Infrastructure.Queries.Handling;
using NetFlow.Queries.Security.Dto;
using NetFlow.Queries.Security.Requests;

namespace NetFlow.Queries.Security.Handlers
{
    public class UserRequestHandler : IRequestHandler<FindUserByLogin, UserDto>
    {
        private readonly IReadOnlyDbContext _readOnlyDb;

        public UserRequestHandler(IReadOnlyDbContext readOnlyDb)
        {
            if (readOnlyDb == null) throw new ArgumentNullException(nameof(readOnlyDb));
            _readOnlyDb = readOnlyDb;
        }

        public UserDto Handle(FindUserByLogin request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return _readOnlyDb.Get<UserDto, Guid>().FirstOrDefault(u => u.Login == request.Login);
        }
    }
}
