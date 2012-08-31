
namespace SHHH.Infrastructure.Mvc.FormsAuthentication
{
    public class BasicFormsAuthentication : IFormsAuthenticationAdapter
    {
        public void SignOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
        }

        public void SignIn(string username, bool createPersistentCookie)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
        }


        public string LoginUrl
        {
            get { return System.Web.Security.FormsAuthentication.LoginUrl; }
        }


        public string GetRedirectUrl(string username, bool createPersistentCookie)
        {
            return System.Web.Security.FormsAuthentication.GetRedirectUrl(username, createPersistentCookie);
        }
    }
}
