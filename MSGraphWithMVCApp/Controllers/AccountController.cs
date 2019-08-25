using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

using System.Web.Mvc;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;

namespace MSGraphWithMVCApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                
                HttpContext.GetOwinContext().Authentication.Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = "/"
                    }, OpenIdConnectAuthenticationDefaults.AuthenticationType
                );
            }
        }

        public void SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
            OpenIdConnectAuthenticationDefaults.AuthenticationType,
            CookieAuthenticationDefaults.AuthenticationType
            );
        }
    }

}