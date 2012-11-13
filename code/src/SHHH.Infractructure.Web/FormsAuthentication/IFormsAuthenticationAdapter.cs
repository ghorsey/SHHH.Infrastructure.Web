
namespace SHHH.Infrastructure.Web.FormsAuthentication
{
    public interface IFormsAuthenticationAdapter
    {
        void SignOut();
        void SignIn(string username, bool createPersistentCookie);
        string LoginUrl { get; }
        string GetRedirectUrl(string username, bool createPersistentCookie);

    }
}
