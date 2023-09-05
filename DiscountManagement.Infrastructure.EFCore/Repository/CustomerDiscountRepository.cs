using _0_Framework.Application;
using _0_Framework.Domain.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository :RepositoryBase<long,CustomerDiscount>, ICustomerDiscountRepository
    {

        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext context,ShopContext shopContext) : base(context) 
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x=> new EditCustomerDiscount 
            {
                Id = id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                StartDate=x.StartDate.ToString(),
                EndDate=x.EndDate.ToString(),
                Reason = x.Reason,
            })
                .FirstOrDefault(x=>x.Id==id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products =_shopContext.Products.Select(x=>new {x.Id,x.Name}).ToList();
            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                EndDateGr=x.EndDate,
                StartDate = x.StartDate.ToFarsi(),
                StartDateGr= x.StartDate,
                ProductId = x.ProductId,
                Reason = x.Reason,
                CreationDate =x.CreationDate.ToFarsi()
                
                
            });

            if(searchModel.ProductId>0)
                query = query.Where(x=>x.ProductId==searchModel.ProductId); 

            if(!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                var endDate = DateTime.Now;
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(x=>x.Id ).ToList();

            discounts.ForEach(discount => discount.Product =products
            .FirstOrDefault(x=> x.Id==discount.ProductId)?.Name);

            return discounts;
        }
    }
}
