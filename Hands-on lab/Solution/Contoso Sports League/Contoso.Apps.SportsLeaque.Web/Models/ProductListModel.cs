namespace Contoso.Apps.SportsLeague.Web.Models
{
    public class ProductListModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string ImagePath { get; set; }

        public string ThumbnailPath { get; set; }

        public double? UnitPrice { get; set; }

        public int? CategoryID { get; set; }
    }
}