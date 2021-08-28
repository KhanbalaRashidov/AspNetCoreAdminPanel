using AspNetCoreAdminPanel.UI.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.UI.Controllers
{
    public class SecurityController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<IdentityUser> _signInManager;
        private static IConfiguration _configuration;

        public SecurityController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Confirm your email please!");
                        return View(loginViewModel);
                    }
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, true);
                    if (result.Succeeded)
                    {
                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return View(loginViewModel);
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Login failed!");
                    return View(loginViewModel);
                }
                return View(loginViewModel);
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var projectUrl = _configuration.GetSection("ProjectSettings").GetSection("ProjectUrl").Value;
                    var callBackUrl = projectUrl + Url.Action("ConfirmEmail", "Security", new { userId = user.Id, code = confirmationCode });

                    return RedirectToAction("ConfirmEmailInfo","Security", new { email = user.Email });
                }
                return View(registerViewModel);
            }
            return View(registerViewModel);
        }

        public IActionResult ConfirmEmailInfo(string email)
        {
            TempData["email"] = email;
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController), nameof(Index));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found!");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordViewModel);
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
            if (user == null)
            {
                return View(forgotPasswordViewModel);
            }
            var confirmationCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var projectUrl = _configuration.GetSection("ProjectSettings").GetSection("ProjectUrl").Value;
            var callBack = projectUrl + Url.Action("ResetPassword", "Security", new { userId = user.Id, code = confirmationCode });


            //Send Email
            return RedirectToAction(nameof(ConfirmForgotPasswordInfo), new { email = forgotPasswordViewModel.Email });
        }

        public IActionResult ConfirmForgotPasswordInfo(string email)
        {
            TempData["email"] = email;
            return View();
        }

        public IActionResult ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
            {
                throw new ApplicationException("User id or code  must be supplied for password reset!!");
            }
            var resetPasswordViewModel = new ResetPasswordViewModel
            {
                Code = code,

            };
            return View(resetPasswordViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user == null)
                {
                    throw new ApplicationException("User not found");
                }
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                return View(resetPasswordViewModel);
            }
            return View(resetPasswordViewModel);
        }

       
    }
}
