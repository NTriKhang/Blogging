using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Infrastructure.Identity
{
    internal class KeyCloakOptions
    {
        public string AdminUrl { set; get; }
        public string TokenUrl { set; get; }
        public string ConfidentialClientId { set; get; }
        public string ConfidentialClientSecret { set; get; }
        public string PublicClientId { set; get; }
    }
}
