using Blogging.Common.Domain;

namespace Blogging.Modules.Blog.Domain.Contributes
{
    public sealed class ContributeContent : Entity
    {
        public Guid Id { get; private set;} 
        public Guid ContributeId { get; private set; }
        public string Content { get; private set; }
        public DateTime CDate { get; private set; }

        public static ContributeContent Create(
            Guid ContributeId
            , string content)
        {
            var contribute = new ContributeContent()
            {
                Id = Guid.NewGuid(),
                ContributeId = ContributeId,
                Content = content,
                CDate = DateTime.UtcNow,
            };

            return contribute;
        }
    }
}
