using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
	public class LoginController : Controller
	{
		LogHelper _logger = new LogHelper("Login");
		TicketService _service;
		public LoginController(TicketService TicketService)
		{
			_service = TicketService;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(Account acc)
		{
			var account = _service.GetAccount(acc.LoginAccount);
			if(account != null)
			{
				var permissions = _service.GetPermissions(account.Role);
				var claimsIdentity = new ClaimsIdentity(new List<Claim>
				{
					new Claim(ClaimTypes.Name, account.Name ?? account.LoginAccount),
					new Claim("UserModel", JsonSerializer.Serialize(account)),
					new Claim(ClaimTypes.Role, "User"),
					new Claim("p", JsonSerializer.Serialize(permissions))
				}, CookieAuthenticationDefaults.AuthenticationScheme);

				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				new AuthenticationProperties()
				{
					IsPersistent = true,
					ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
					AllowRefresh = true
				});

				_logger.Info("Login", $"Login User:{JsonSerializer.Serialize(account)}");
				return RedirectToAction("Index", "Ticket");
			}
			return View("Index",acc);
		}

		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index");
		}
	}
}
