using ShoppingWeb.Repository.Models;
using ShoppingWeb.Services.ViewModels.cs;

namespace ShoppingWeb.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account> LoginAsync(string UserName, string Password);
    }
}
