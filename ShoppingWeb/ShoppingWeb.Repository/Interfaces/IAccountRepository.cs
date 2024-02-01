using System;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Repository.Interfaces
{
	public interface IAccountRepository : IGenericRepository<Account>
	{
        Task<Account> GetUserByAccountNameAndPasswordHash(string userName, string passWord);

        Task<bool> CheckAccountNameExited(string userName);

    }
}

