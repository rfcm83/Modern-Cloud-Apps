using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Contoso.Apps.SportsLeague.Web.Models
{
    public class CheckoutModel
    {
        public IList<Models.CartItemModel> CartItems { get; set; }
        public Models.OrderModel Order { get; set; }
        public decimal OrderTotal { get; set; }
        public int ItemsTotal { get; set; }

        public IEnumerable<SelectListItem> CreditCardTypes
        {
            get
            {
                var cardTypes = new List<string>();
                cardTypes.Add("");
                cardTypes.Add("Master Card");
                cardTypes.Add("Visa");
                cardTypes.Add("Discover");
                cardTypes.Add("American Express");

                return new SelectList(cardTypes);
            }
        }
    }
}