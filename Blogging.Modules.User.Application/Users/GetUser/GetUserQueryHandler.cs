using Blogging.Common.Application.Data;
using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.User.Domain.Users;
using Dapper;
using System.Data.Common;

namespace Blogging.Modules.User.Application.Users.GetUser
{
    internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetUserQuery, GetUserResponse>
    {
        public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            $"""
             SELECT
                 "Id" AS {nameof(GetUserResponse.Id)},
                 "UserName" AS {nameof(GetUserResponse.UserName)},
                 "DisplayName" AS {nameof(GetUserResponse.DisplayName)},
                 "Email" AS {nameof(GetUserResponse.Email)},
                 "ImageUrl" AS {nameof(GetUserResponse.ImageUrl)}
             FROM "users"."Users"
             WHERE "Id" = @UserId
             """;

            GetUserResponse? user = await connection.QuerySingleOrDefaultAsync<GetUserResponse>(sql, request);
            if (user is null)
            {
                return Result.Failure<GetUserResponse>(UserErrors.NotFound(request.UserId));
            }
            return user;
        }
    }
}
