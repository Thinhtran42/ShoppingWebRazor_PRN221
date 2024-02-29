using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingWeb.Razor.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
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
}
