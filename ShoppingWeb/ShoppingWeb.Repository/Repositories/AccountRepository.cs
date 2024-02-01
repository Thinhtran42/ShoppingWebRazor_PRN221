using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Repository.Repositories
{
	public class AccountRepository : GenericRepository<Account>, IAccountRepository
	{
        private readonly ShoppingWebRazorDatabaseContext _dbContext;

        public AccountRepository(ShoppingWebRazorDatabaseContext dbContext) : base(dbContext)
		{
            _dbContext = dbContext;
		}

        public Task<bool> CheckAccountNameExited(string userName)
        {
           return _dbContext.Accounts.AnyAsync(u => u.UserName == userName);
        }

        public async Task<Account> GetUserByAccountNameAndPasswordHash(string userName, string passWord)
        {
            var user = await _dbContext.Accounts
               .FirstOrDefaultAsync(record => record.UserName == userName
                                       && record.Password == passWord);
            if (user is null)
            {
                throw new Exception("UserName & password is not correct");
            }

            return user;

        }
    }
}

