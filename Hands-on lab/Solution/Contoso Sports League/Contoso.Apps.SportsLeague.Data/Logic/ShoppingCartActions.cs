using Contoso.Apps.SportsLeague.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Apps.SportsLeague.Data.Logic
{
    public class ShoppingCartActions
    {
        private readonly ProductContext _db;

        public ShoppingCartActions(ProductContext context, string cartId)
        {
            _db = context;

            this.ShoppingCartId = cartId;
        }

        public string ShoppingCartId { get; private set; }

        public async Task AddToCart(int id)
        {
            // Retrieve the product from the database.           
            //ShoppingCartId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == id);

            if (cartItem == null) {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(
                     p => p.ProductID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                await _db.ShoppingCartItems.AddAsync(cartItem);
            }
            else {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            await _db.SaveChangesAsync();
        }

        public List<CartItem> GetCartItems()
        {
            //ShoppingCartId = GetCartId();

            return (from c in _db.ShoppingCartItems.Include("Product")
                   where c.CartId == ShoppingCartId
                   select c).ToList();
        }

        public decimal GetTotal()
        {
            //ShoppingCartId = GetCartId();
            // Multiply product price by quantity of that product to get        
            // the current price for each of those products in the cart.  
            // Sum all product price totals to get the cart total.   
            decimal? total = decimal.Zero;

            total = (decimal?)(from cartItems in _db.ShoppingCartItems
                               where cartItems.CartId == ShoppingCartId
                               select (int?)cartItems.Quantity *
                               cartItems.Product.UnitPrice).Sum();

            return total ?? decimal.Zero;
        }

        public async Task UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[] CartItemUpdates)
        {
            try
            {
                int CartItemCount = CartItemUpdates.Count();
                List<CartItem> myCart = GetCartItems();
                foreach (var cartItem in myCart)
                {
                    // Iterate through all rows within shopping cart list
                    for (int i = 0; i < CartItemCount; i++)
                    {
                        if (cartItem.Product.ProductID == CartItemUpdates[i].ProductId)
                        {
                            if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem == true)
                            {
                                await RemoveItem(cartId, cartItem.ProductId);
                            }
                            else
                            {
                                await UpdateItem(cartId, cartItem.ProductId, CartItemUpdates[i].PurchaseQuantity);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Update Cart Database - " + exp.Message.ToString(), exp);
            }
        }

        public async Task RemoveItem(string removeCartID, int removeProductID)
        {
            try
            {
                var myItem = (from c in _db.ShoppingCartItems where c.CartId == removeCartID && c.Product.ProductID == removeProductID select c).FirstOrDefault();
                if (myItem != null)
                {
                    // Remove Item.
                    _db.ShoppingCartItems.Remove(myItem);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
            }
        }

        public async Task UpdateItem(string updateCartID, int updateProductID, int quantity)
        {
            try
            {
                var myItem = (from c in _db.ShoppingCartItems where c.CartId == updateCartID && c.Product.ProductID == updateProductID select c).FirstOrDefault();
                if (myItem != null)
                {
                    myItem.Quantity = quantity;
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
            }
        }

        public async Task EmptyCart()
        {
            //ShoppingCartId = GetCartId();
            var cartItems = _db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _db.ShoppingCartItems.Remove(cartItem);
            }

            // Save changes.             
            await _db.SaveChangesAsync();
        }

        public int GetCount()
        {
            //ShoppingCartId = GetCartId();

            // Get the count of each item in the cart and sum them up          
            var count = (from cartItems in _db.ShoppingCartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();

            // Return 0 if all entries are null         
            return count ?? 0;
        }

        public struct ShoppingCartUpdates
        {
            public int ProductId;
            public int PurchaseQuantity;
            public bool RemoveItem;
        }
    }
}