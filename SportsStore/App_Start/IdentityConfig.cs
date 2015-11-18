using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SportsStore.Infrastructure.Identity;

[assembly: OwinStartup(typeof(SportsStore.IdentityConfig))]

namespace SportsStore
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<StoreIdentityDbContext>(StoreIdentityDbContext.Create);
            app.CreatePerOwinContext<StoreUserManager>(StoreUserManager.Create);
            app.CreatePerOwinContext<StoreRoleManager>(StoreRoleManager.Create);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            //});

            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions()
            {
                Provider = new StoreAuthProvider(),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Authenticate")  
            });

            //to test authenticate: postman localhost:6100/authenticate POST
            //x-www-form-urlencoded
            //grant_type: password
            //username: Bob (Admin is working now)
            //password: secret
        }
    }
}
