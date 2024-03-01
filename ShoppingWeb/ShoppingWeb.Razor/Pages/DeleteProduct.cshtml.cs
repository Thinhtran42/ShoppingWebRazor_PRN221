using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Razor.Pages
{
    public class DeleteProductModel : PageModel
    {
        protected IUnitOfWork _unitOfWork;

        [BindProperty(SupportsGet = true)]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DeleteProductModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult OnGet()
        {
            Product = _unitOfWork.ProductRepository.GetByID(ProductId);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Product = _unitOfWork.ProductRepository.GetByID(ProductId);

            Product.IsDeleted = true;

            _unitOfWork.ProductRepository.Update(Product);
            _unitOfWork.Save();

            return RedirectToPage("/Pizza");
        }
    }
}
