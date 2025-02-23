using Blogging.Common.Application.Data;
using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Blogs.GetBlog;
using Dapper;

namespace Blogging.Modules.Blog.Application.Blogs.GetBlogs
{
    internal sealed class GetBlogsQueryHandler(
        IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBlogsQuery, BlogsResponse>
    {
        public async Task<Result<BlogsResponse>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            $"""
             SELECT
                 "Id" AS {nameof(BlogResponse.Id)},
                 "UserId" AS {nameof(BlogResponse.UserId)},
                 "Title" AS {nameof(BlogResponse.Title)},
                 "Description" AS {nameof(BlogResponse.Description)},
                 "CDate" AS {nameof(BlogResponse.CDate)},
                 "UDate" AS {nameof(BlogResponse.UDate)},
                 "ThumbnailUrl" AS {nameof(BlogResponse.ThumbnailUrl)},
                 "Like" AS {nameof(BlogResponse.Like)},
                 "Dislike" AS {nameof(BlogResponse.Dislike)},
                 "State" AS {nameof(BlogResponse.State)}
             FROM "Blog"."Blogs"
             WHERE "IsPublicVisible" = true
             ORDER BY "UDate" DESC
             LIMIT @PageSize OFFSET @Offset
             """;

            const string countSql =
            """
            SELECT COUNT(*) 
            FROM "Blog"."Blogs"
            WHERE "IsPublicVisible" = true
            """;

            var parameters = new { PageSize = request.pageSize, Offset = (request.page - 1) * request.pageSize };
            var blogs = await connection.QueryAsync<BlogResponse>(sql, parameters);
            var totalBlogs = await connection.ExecuteScalarAsync<int>(countSql);

            var totalPages = (int)Math.Ceiling(totalBlogs / (double)request.pageSize);
            var response = new BlogsResponse(blogs, request.page, request.pageSize, totalPages);

            return Result.Success(response);
        }
    }
}
