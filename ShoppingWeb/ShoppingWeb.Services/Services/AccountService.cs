using System;
using Microsoft.Extensions.Configuration;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;
using ShoppingWeb.Services.Interfaces;
using ShoppingWeb.Services.ViewModels.cs;

namespace ShoppingWeb.Services.Services
{
	public class AccountService : IAccountService
	{
        private readonly IUnitOfWork _unitOfWork;

		public AccountService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public async Task<Account> LoginAsync(string UserName, string Password)
        {
            var user = await _unitOfWork.AccountRepository.GetUserByAccountNameAndPassword(UserName, Password);
            return user;
        }
    }
}

