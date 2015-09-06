using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using MediaCommMvc.Web.ViewModels;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class AccountController : Controller
    {
        public AccountController()
        {
        }

        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.View(model);


            //// This doesn't count login failures towards account lockout
            //// To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await this.SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return this.RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return this.View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return this.RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });
            //    default:
            //        this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //        return this.View(model);
            //}
        }


        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            //if (this.ModelState.IsValid)
            //{
            //    var user = new ApplicationUser { UserName = model.Username, Email = model.Email, Id = model.Username };
            //    var result = await this.UserManager.CreateAsync(user, model.Password);
            //    if (result.Succeeded)
            //    {
            //        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //        // Send an email with this link
            //        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            //        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            //        return this.RedirectToAction("Index", "Home");
            //    }

            //    this.AddErrors(result);
            //}

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            //if (this.ModelState.IsValid)
            //{
            //    var user = await this.UserManager.FindByNameAsync(model.Email);
            //    if (user == null || !(await this.UserManager.IsEmailConfirmedAsync(user.Id)))
            //    {
            //        // Don't reveal that the user does not exist or is not confirmed
            //        return this.View("ForgotPasswordConfirmation");
            //    }

            //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //    // Send an email with this link
            //    // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            //    // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
            //    // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            //    // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            //}

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }

        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string code)
        {
            return code == null ? this.View("Error") : this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return this.View(model);
            //}

            //var user = await this.UserManager.FindByNameAsync(model.Email);
            //if (user == null)
            //{
            //    // Don't reveal that the user does not exist
            //    return this.RedirectToAction("ResetPasswordConfirmation", "Account");
            //}

            //var result = await this.UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            //if (result.Succeeded)
            //{
            //    return this.RedirectToAction("ResetPasswordConfirmation", "Account");
            //}

            //for

            //this.AddErrors(result);
            return this.View();
        }

        [AllowAnonymous]
        public virtual ActionResult ResetPasswordConfirmation()
        {
            return this.View();
        }

     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOff()
        {
            //this.AuthenticationManager.SignOut();
            return this.RedirectToAction("Index", "Home");
        }


        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
       

        #endregion
    }
}