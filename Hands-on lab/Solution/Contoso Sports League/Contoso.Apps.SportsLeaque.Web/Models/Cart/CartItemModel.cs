namespace Contoso.Apps.SportsLeague.Web.Models
{
    public class CartItemModel
    {
        public string ItemId { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int ProductId { get; set; }

        public string ProductProductName { get; set; }

        public string ProductThumbnailPath { get; set; }

        public double? ProductUnitPrice { get; set; }
    }
}