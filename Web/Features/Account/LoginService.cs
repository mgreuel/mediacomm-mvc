using System.Security.Claims;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace MediaCommMvc.Web.Features.Account
{
    public class LoginService
    {
        private readonly IAuthenticationManager authenticationManager;

        public LoginService(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public void SignIn(string username, bool rememberLogin)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.Name, username));

            if(rememberLogin)
            {
                ClaimsIdentity rememberBrowserIdentity = this.authenticationManager.CreateTwoFactorRememberBrowserIdentity(username);

                this.authenticationManager.SignIn(
                    new AuthenticationProperties { IsPersistent = true,  },
                    identity,
                    rememberBrowserIdentity);
            }
            else
            {
                this.authenticationManager.SignIn(
                    new AuthenticationProperties { IsPersistent = true },
                    identity);
            }
        }

        public void SignOut()
        {
            this.authenticationManager.SignOut();
        }
    }
}
