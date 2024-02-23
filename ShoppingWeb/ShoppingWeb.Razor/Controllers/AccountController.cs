using System;
using ShoppingWeb.Repository.Models;
using ShoppingWeb.Services.Interfaces;
using ShoppingWeb.Services.Services;
using ShoppingWeb.Services.ViewModels.cs;

namespace ShoppingWeb.Razor.Controllers
{
	public class AccountController : BaseController<AccountController>
	{
		private readonly IAccountService _accountService;
		public AccountController(ILogger<AccountController> logger,IAccountService accountService) : base(logger)
		{
			_accountService = accountService;
        }

        public async Task<Account> LoginAsync(string userName, string passWord)
        {
            var loginAccount = await _accountService.LoginAsync(userName, passWord);
            if (loginAccount is null)
            {
                ViewBag.ErrorMessage = "Log in failed.";
                return null;
            }
            return loginAccount;
        }
    }
}

