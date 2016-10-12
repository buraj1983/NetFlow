using System;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Infrastructure.Database;
using NetFlow.Infrastructure.Queries;
using NetFlow.Queries.Security.Dto;
using NetFlow.Queries.Security.Queries;

namespace NetFlow.Queries.Security.Handlers
{
    public class UserQueryHandler : IQueryHandler<FindUserByLogin, UserDto>,
        IQueryHandler<GetUsers, IEnumerable<UserDto>>
    {
        private readonly IReadOnlyDbContext _readOnlyDb;

        public UserQueryHandler(IReadOnlyDbContext readOnlyDb)
        {
            if (readOnlyDb == null) throw new ArgumentNullException(nameof(readOnlyDb));
            _readOnlyDb = readOnlyDb;
        }

        UserDto IQueryHandler<FindUserByLogin, UserDto>.Handle(FindUserByLogin query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            return _readOnlyDb.Get<UserDto, Guid>().FirstOrDefault(u => u.Login == query.Login);
        }

        IEnumerable<UserDto> IQueryHandler<GetUsers, IEnumerable<UserDto>>.Handle(GetUsers query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            return _readOnlyDb.Get<UserDto, Guid>().ToList();
        }
    }
}
