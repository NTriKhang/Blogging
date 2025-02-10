using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Domain.Users
{
    public class Permission
    {
        public string Code { get; }
        public Permission(string code)
        {
            Code = code;
        }
    }
}
