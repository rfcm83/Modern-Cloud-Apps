﻿using System.Collections.Generic;

namespace Contoso.Apps.SportsLeague.Web.Models
{
    public class StoreIndexModel
    {
        // List of products.
        public List<ProductListModel> Products { get; set; }
        // List of categories used to filter through the products.
        public List<CategoryModel> Categories { get; set; }
    }
}