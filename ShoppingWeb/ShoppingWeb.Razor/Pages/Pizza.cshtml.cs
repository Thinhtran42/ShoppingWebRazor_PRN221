using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Razor.Pages
{
	public class PizzaModel : PageModel
    {
        protected IUnitOfWork _unitOfWork;
        public IEnumerable<Product> Products { get; set; }

        public PizzaModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            Products = _unitOfWork.ProductRepository.Get(includeProperties: "Category");
            return Page();
        }
    }
}
