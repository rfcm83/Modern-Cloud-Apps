using AutoMapper;
using Contoso.Apps.SportsLeague.Data.Logic;
using Contoso.Apps.SportsLeague.Data.Models;
using Contoso.Apps.SportsLeague.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Contoso.Apps.SportsLeague.Web.Controllers
{
    public class CartController : Controller
    {
        public CartController(ProductContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        private readonly ProductContext _db;
        private readonly IMapper _mapper;

        // GET: Cart
        public ActionResult Index()
        {
            var cartId = new Helpers.CartHelpers().GetCartId(HttpContext);
            var vm = new CartModel();

            var usersShoppingCart = new ShoppingCartActions(_db, cartId);

            var shoppingCartItems = usersShoppingCart.GetCartItems();
            var cartItemsVM = _mapper.Map<List<CartItemModel>>(shoppingCartItems);
            vm.CartItems = cartItemsVM;
            vm.ItemsTotal = usersShoppingCart.GetCount();
            vm.OrderTotal = usersShoppingCart.GetTotal();
            
            //var shoppingCartItems = db.ShoppingCartItems.Include(c => c.Product);
            return View(vm);
        }

        public async Task<RedirectResult> AddToCart(int productId)
        {
            var cartId = new Helpers.CartHelpers().GetCartId(HttpContext);

            var usersShoppingCart = new ShoppingCartActions(_db, cartId);
            
            await usersShoppingCart.AddToCart(productId);
            
            return Redirect("Index");
        }

        // GET: Cart/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var cartItem = _db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ItemId,CartId,Quantity,DateCreated,ProductId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _db.ShoppingCartItems.Add(cartItem);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(_db.Products, "ProductID", "ProductName", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CartItem cartItem = _db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewBag.ProductId = new SelectList(_db.Products, "ProductID", "ProductName", cartItem.ProductId);
            return View(cartItem);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("ItemId,CartId,Quantity,DateCreated,ProductId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cartItem).State = EntityState.Modified;
                _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_db.Products, "ProductID", "ProductName", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CartItem cartItem = _db.ShoppingCartItems.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CartItem cartItem = _db.ShoppingCartItems.Find(id);
            _db.ShoppingCartItems.Remove(cartItem);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
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
