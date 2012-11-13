using System.Collections.Generic;

namespace SHHH.Infrastructure.Web.Tests.Shims
{
    public class User
    {
        public User()
        {
            this.Name = "Geoff";
            this.Roles = new List<string>
            {
                "manager"
            };
        }

        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
