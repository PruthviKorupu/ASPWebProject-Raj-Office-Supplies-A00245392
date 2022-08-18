using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DAL.Entities;
using RajOfficeSupplies.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using BLL.Interfaces;

namespace RajOfficeSupplies.Controllers
{
	public class AccountController : Controller
	{
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;
        ICartService _cartService;

		public AccountController(UserManager<UserEntity> userManager,
			SignInManager<UserEntity> signInManager,
			IConfiguration configuration, ICartService cartService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
			_cartService = cartService;
		}

		[HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.SetString(CartController.CartSessionKey, "");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(NewUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    FullName = model.FullName,
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token }, Request.Scheme);

                    SendEmailCustom(confirmationLink, model.Email, "Email confirmation link");

                    return View("RegistrationConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, false);

                if (result.Succeeded)
                {
                    _cartService.UpdateCartId(HttpContext.Session.GetString(CartController.CartSessionKey), model.Email);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid Login Attempt");
            }

            return View(model);
        }

       
        public void SendEmailCustom(string body, string email, string subject)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("testweblab6@gmail.com");
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("testweblab6@gmail.com", "tktivfvbhqbldrtr");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
