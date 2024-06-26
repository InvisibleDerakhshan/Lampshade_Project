using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;
        public ProductModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
            
        }
        public void OnGet(string id)
        {
            Product = _productQuery.GetProductDetails(id);
        }
        public IActionResult OnPost(string productSlug)
        {    
            return RedirectToPage("/Product", new {Id = productSlug });
        }
    }
}

