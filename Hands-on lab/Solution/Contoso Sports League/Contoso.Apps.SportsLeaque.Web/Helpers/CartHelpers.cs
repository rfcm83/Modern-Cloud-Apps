using Contoso.Apps.SportsLeague.Data.Logic;
using Contoso.Apps.SportsLeague.Data.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace Contoso.Apps.SportsLeague.Web.Helpers
{
    // Helpful Links:
    // https://social.technet.microsoft.com/wiki/contents/articles/51526.asp-net-core-sessions.aspx

    public class CartHelpers {
        const string CartSessionKey = "CartId";

        public ShoppingCartActions GetCart(HttpContext context, ProductContext db) {
            var cartId = GetCartId(context);
            return new ShoppingCartActions(db, cartId);
        }

        public string GetCartId(HttpContext context) {
            if (context.Session.GetString(CartSessionKey) == null) {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name)) {
                    context.Session.SetString(CartSessionKey, context.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return context.Session.GetString(CartSessionKey);
        }
    }
}