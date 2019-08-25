using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Configuration;

[assembly: OwinStartup(typeof(MSGraphWithMVCApp.App_Start.Startup))]

namespace MSGraphWithMVCApp.App_Start
{
    public class Startup
    {
        private static string Client_ID = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string AzureADInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string YourOffice365Tenent = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string LogoutRedirect = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];

        string authority = String.Format(CultureInfo.InvariantCulture, AzureADInstance, YourOffice365Tenent);

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
            new OpenIdConnectAuthenticationOptions
            {
                ClientId = Client_ID,
                Authority = authority,
                PostLogoutRedirectUri = LogoutRedirect,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthenticationFailed = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/ Error / message =" +context.Exception.Message);
                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}