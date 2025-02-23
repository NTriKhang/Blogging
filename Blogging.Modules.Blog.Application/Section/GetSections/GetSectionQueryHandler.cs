using Blogging.Common.Application.Data;
using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Section.GetSection;
using Dapper;

namespace Blogging.Modules.Blog.Application.Section.GetSections
{
    internal sealed class GetSectionQueryHandler(IDbConnectionFactory dbConnectionFactory) 
        : IQueryHandler<GetSectionsQuery, SectionsResponse>
    {
        public async Task<Result<SectionsResponse>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
        {
            var connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            $"""
             SELECT
                 "Id" AS {nameof(SectionResponse.Id)},
                 "BlogId" AS {nameof(SectionResponse.BlogId)},
                 "Title" AS {nameof(SectionResponse.Title)},
                 "Content" AS {nameof(SectionResponse.Content)},
                 "Order" AS {nameof(SectionResponse.Order)},
                 "CDate" AS {nameof(SectionResponse.CDate)},
                 "UDate" AS {nameof(SectionResponse.UDate)}
             FROM "Blog"."Sections"
             WHERE "BlogId" = @BlogId
             ORDER BY "Order"
             LIMIT @PageSize OFFSET @Offset
             """;

            const string countSql =
            """
            SELECT COUNT(*) 
            FROM "Blog"."Sections"
            """;

            var parameters = new { BlogId = request.BlogId, PageSize = request.PageSize, Offset = (request.Page - 1) * request.PageSize };
            
            var sections = await connection.QueryAsync<SectionResponse>(sql, parameters);
            var totalBlogs = await connection.ExecuteScalarAsync<int>(countSql);

            var totalPages = (int)Math.Ceiling(totalBlogs / (double)request.PageSize);
            var response = new SectionsResponse(sections, request.Page, request.PageSize, totalPages);

            return Result.Success(response);
        }
    }
}
