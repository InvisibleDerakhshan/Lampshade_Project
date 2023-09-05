using _01_LampshadeQuery.Contract.ProductCategory;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
           _context = context;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.productCategories.Select(x => new ProductCategoryQueryModel 
            { 
                Id = x.Id,
                Name = x.Name,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Slug=x.Slug,
            }).ToList();    
        }
    }
}
