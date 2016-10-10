using System;
using System.Linq;
using NetFlow.Infrastructure.Database;
using NetFlow.Queries.Dtos.Security;
using NetFlow.Queries.Requests.Security;

namespace NetFlow.Queries.Handlers.Security
{
    public class UserRequestHandler : IRequestHandler<FindUserByLogin, User>
    {
        private readonly IReadOnlyDbContext _readOnlyDb;

        public UserRequestHandler(IReadOnlyDbContext readOnlyDb)
        {
            if (readOnlyDb == null) throw new ArgumentNullException(nameof(readOnlyDb));
            _readOnlyDb = readOnlyDb;
        }

        public User Handle(FindUserByLogin request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return _readOnlyDb.Get<User, Guid>().FirstOrDefault(u => u.Login == request.Login);
        }
    }
}
