using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Razor.Pages;

public class IndexModel : PageModel
{
    protected IUnitOfWork _unitOfWork;

    public IEnumerable<Product> Products { get; set; }

    public IndexModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet(string action, string searchInput)
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

        if (string.IsNullOrWhiteSpace(searchInput))
        {
            Products = _unitOfWork.ProductRepository.Get(includeProperties: "Category", filter: p => p.IsDeleted == false);
        }
        else
        {
            Products = _unitOfWork.ProductRepository.Get(includeProperties: "Category", filter: p => p.IsDeleted == false && p.ProductName.Contains(searchInput.Trim()));
        }

        return Page();
    }
}
