using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Repository.Interfaces;

namespace ShoppingWeb.Razor.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public string UserName { get; set; } = "";
        [BindProperty]
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; }
        public RegisterModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(string action)
        {
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "logout":
                        HttpContext.Session.Remove("loginUser");
                        return RedirectToPage("/Index");
                    default: break;
                }
            }

            return Page();
        }

        // public async Task<IActionResult> OnPostAsync()
        // {

        // }
    }
}