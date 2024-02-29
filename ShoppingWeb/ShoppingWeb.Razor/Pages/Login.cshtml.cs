
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShoppingWeb.Razor.ViewModels;
using ShoppingWeb.Repository.Interfaces;

namespace ShoppingWeb.Razor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public string UserName { get; set; } = "";
        [BindProperty]
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; }

        public LoginModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("loginUser") != null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Username and Password must not be empty. Please try again.";
                    return Page();
                }
                var loginAccount = _unitOfWork.AccountRepository.Get(a => a.UserName == UserName && a.Password == Password).FirstOrDefault();
                if (loginAccount == null)
                {
                    ErrorMessage = "Invalid username or password. Please try again.";
                    return Page();
                }
                var loginUser = new AccountViewmodel
                {
                    AccountId = loginAccount.AccountId,
                    UserName = loginAccount.UserName,
                    Type = loginAccount.Type,
                };

                var loginUserJson = JsonConvert.SerializeObject(loginUser);
                HttpContext.Session.SetString("loginUser", loginUserJson);

                if (loginAccount.Type == "Staff")
                {
                    return RedirectToPage("/Pizza");
                }

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }
        }
    }
}
