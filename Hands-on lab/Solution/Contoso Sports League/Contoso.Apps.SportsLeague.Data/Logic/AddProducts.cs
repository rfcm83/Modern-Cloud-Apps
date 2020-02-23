using Contoso.Apps.SportsLeague.Data.Models;
using System;
using System.Threading.Tasks;

namespace Contoso.Apps.SportsLeague.Data.Logic
{
    public class AddProducts
    {
        private readonly ProductContext _context;

        public AddProducts(ProductContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(string ProductName, string ProductDesc, string ProductPrice, string ProductCategory, string ProductImagePath)
        {
            var myProduct = new Product()
            {
                ProductName = ProductName,
                Description = ProductDesc,
                UnitPrice = Convert.ToDouble(ProductPrice),
                ImagePath = ProductImagePath,
                CategoryID = Convert.ToInt32(ProductCategory),
            };

            // Add product to DB.
            await _context.Products.AddAsync(myProduct);
            await _context.SaveChangesAsync();
            
            // Success.
            return true;
        }
    }
}