using Blogging.Modules.Blog.Domain.Contributes;
using Blogging.Modules.Blog.Infrastructure.Database;

namespace Blogging.Modules.Blog.Infrastructure.Contribute
{
    internal class ContributeContentRepository(BlogDbContext blogDbContext) : IContributeContentRepository
    {
        public void Insert(ContributeContent contributeContent)
        {
            blogDbContext.ContributeContents.Add(contributeContent);
        }
    }
}
