﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Contoso.Apps.SportsLeague.Data.Models
{
    [Serializable]
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string ProductName { get; set; }

        [Required, StringLength(10000), Display(Name = "Product Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string ThumbnailPath { get; set; }

        [Display(Name = "Price")]
        public double? UnitPrice { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }

    // This class is used to compare two objects of type Product to remove 
    // all objects that are duplicates, as determined by the ProductID field.
    public class ProductsComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (x.ProductID == y.ProductID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Product obj)
        {
            return obj.ProductID.GetHashCode();
        }
    }
}