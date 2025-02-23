using System.Linq.Expressions;

namespace Blogging.Modules.Blog.Domain.Contributes
{
    public interface IContributeContentRepository
    {
        void Insert(ContributeContent contribute);
    }
}
