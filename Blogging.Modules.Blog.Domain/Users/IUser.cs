using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Users
{
    public interface IUser
    {
        public Guid Id { get;  }
        public string UserName { get;  }
        public string DisplayName { get;  }
        public string Email { get;  }
        public string ImageUrl { get; }
    }
}
