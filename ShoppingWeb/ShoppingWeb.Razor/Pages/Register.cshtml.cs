using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShoppingWeb.Razor.ViewModels;
using ShoppingWeb.Repository.Enums;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Razor.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Fullname { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string Phone { get; set; }

        public string ErrorMessage { get; set; }
        public RegisterModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(string action)
        {
            if (HttpContext.Session.GetString("loginUser") != null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password) ||
               string.IsNullOrWhiteSpace(Fullname) || string.IsNullOrWhiteSpace(Address) ||
               string.IsNullOrWhiteSpace(Phone))
            {
                ErrorMessage = "All fields must not be empty. Please try again.";
                return Page();
            }

            Expression<Func<Account, bool>> searchFilter = p =>
                p.UserName.Trim().Equals(UserName.Trim());

            Account account = _unitOfWork.AccountRepository.Get(filter: searchFilter).FirstOrDefault();

            if (account != null)
            {
                ErrorMessage = "Username already exists. Please try again.";
                return Page();
            }

            var registerAccount = new Account
            {
                UserName = UserName,
                Password = Password,
                FullName = Fullname,
                Type = AccountType.Member.ToString(),
            };

            _unitOfWork.AccountRepository.Insert(registerAccount);
            _unitOfWork.Save();

            var customer = new Customer
            {
                Password = Password,
                ContactName = Fullname,
                Address = Address,
                Phone = Phone,
                AccountId = registerAccount.AccountId
            };
            _unitOfWork.CustomerRepository.Insert(customer);
            _unitOfWork.Save();

            var loginUser = new AccountViewmodel
            {
                AccountId = registerAccount.AccountId,
                UserName = registerAccount.UserName,
                Type = registerAccount.Type,
            };

            var loginUserJson = JsonConvert.SerializeObject(loginUser);
            HttpContext.Session.SetString("loginUser", loginUserJson);

            return RedirectToPage("/Index");
        }
    }
}