using Blogging.Common.Application.Data;
using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Domain.Blogs;
using Dapper;

namespace Blogging.Modules.Blog.Application.Blogs.GetBlog
{
    internal sealed class GetBlogQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBlogQuery, BlogResponse>
    {
        public async Task<Result<BlogResponse>> Handle(GetBlogQuery request, CancellationToken cancellationToken)
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
             WHERE "Id" = @Id
             """;

            BlogResponse? blog = await connection.QuerySingleOrDefaultAsync<BlogResponse>(sql, request);
            if(blog is null)
                return Result.Failure<BlogResponse>(BlogErrors.NotFound(request.Id));
            return blog;
        }
    }
}
