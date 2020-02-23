using AutoMapper;
using Contoso.Apps.Common.Extensions;
using Contoso.Apps.SportsLeague.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace Contoso.Apps.SportsLeague.Web.Controllers
{
    public class StoreController : Controller
    {
        public StoreController(ProductContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        private readonly ProductContext _db;
        private readonly IMapper _mapper;

        // GET: Store
        public ActionResult Index()
        {
            var products = _db.Products; //.Include(p => p.Category);
            var productsVm = _mapper.Map<List<Models.ProductListModel>>(products);

            // Retrieve category listing:
            var categories = _db.Categories.ToList();
            var categoriesVm = _mapper.Map<List<Models.CategoryModel>>(categories);

            var vm = new Models.StoreIndexModel
            {
                Products = productsVm,
                Categories = categoriesVm
            };

            return View(vm);
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var productVm = _mapper.Map<Models.ProductModel>(product);

            // Find related products, based on the category:
            var relatedProducts = _db.Products.Where(p => p.CategoryID == product.CategoryID && p.ProductID != product.ProductID).ToList();
            var relatedProductsVm = _mapper.Map<List<Models.ProductListModel>>(relatedProducts);

            // Retrieve category listing:
            var categories = _db.Categories.ToList();
            var categoriesVm = _mapper.Map<List<Models.CategoryModel>>(categories);

            // Retrieve "new products" as a list of three random products not equal to the displayed one:
            var newProducts = _db.Products.Where(p => p.ProductID != product.ProductID).ToList().Shuffle().Take(3);

            var newProductsVm = _mapper.Map<List<Models.ProductListModel>>(newProducts);

            var vm = new Models.StoreDetailsModel
            {
                Product = productVm,
                RelatedProducts = relatedProductsVm,
                NewProducts = newProductsVm,
                Categories = categoriesVm
            };

            return View(vm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
