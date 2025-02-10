using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Domain.Users
{
    public class Role
    {
        public static readonly Role Administrator = new("Administrator");
        public static readonly Role Reader = new("Reader");
        public static readonly Role Writer = new("Writer");
        public static readonly Role Coop = new("Coop");
        public string Name { get; }
        public Role(string name)
        {
            Name = name;
        }
    }
}
