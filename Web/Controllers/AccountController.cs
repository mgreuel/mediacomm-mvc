﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Models;
using MediaCommMvc.Web.ViewModels;

using Microsoft.AspNet.Identity.Owin;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class AccountController : RavenController
    {
        private readonly LoginService loginService;

        private readonly UserStorage userStorage;

        public AccountController(LoginService loginService, UserStorage userStorage) : base(userStorage)
        {
            this.loginService = loginService;
            this.userStorage = userStorage;
        }

        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Login(LoginViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            User user = this.userStorage.GetUser(input.Username);

            if (user == null || user.ValidatePassword(input.Password) == false)
            {
                this.ModelState.AddModelError(string.Empty, Resources.Login.UsernameOrPasswordWrongErrorMessage);
            }
            
            if (this.ModelState.IsValid)
            {
                this.loginService.SignIn(input.Username, input.RememberMe);
                return this.RedirectToLocal(input.ReturnUrl);
            }

            return this.View(new LoginViewModel { Username = input.Username, ReturnUrl = input.ReturnUrl });
            

            //this.SignInManager.PasswordSignInAsync
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
        public virtual ActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            { 
                return this.View(model);
            }

            var user = new User { UserName = model.Username, Email = model.Email };
            user.SetPassword(model.Password);
            this.userStorage.CreateUser(user);

            this.loginService.SignIn(user.UserName, false);

            return this.RedirectToAction(MVC.Home.Index());
        }

        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
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
        public virtual ActionResult LogOff()
        {
            this.loginService.SignOut();
            return this.RedirectToAction(MVC.Account.Login());
        }


        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
       

        #endregion

        protected ActionResult RedirectToLocal(string url)
        {
            if (this.Url.IsLocalUrl(url))
            {
                return this.Redirect(url);
            }
            else
            {
                return this.RedirectToAction(MVC.Home.Index());
            }
        }
    }
}