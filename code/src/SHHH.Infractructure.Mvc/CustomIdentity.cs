using System;
using System.Security.Principal;

namespace SHHH.Infrastructure.Mvc
{
    public class CustomIdentity<T>: MarshalByRefObject, IIdentity where T: class
    {
        public CustomIdentity(T reference, string name, bool isAuthenticated)
        {
            if (reference == null) throw new ArgumentNullException("references");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Cannot be null or empty", "name");
            this.Reference = reference;
            this.Name = name;
            this.IsAuthenticated = isAuthenticated;
        }


        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated { get; set;  }

        public string Name { get; private set; }

        public T Reference { get; private set; }
    }
}
