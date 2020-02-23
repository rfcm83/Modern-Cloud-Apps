using AutoMapper;
using Contoso.Apps.SportsLeague.Data.Models;
using Contoso.Apps.SportsLeague.Web.Models;
using System.Collections.Generic;

namespace Contoso.Apps.SportsLeaque.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CartItem, CartItemModel>();
            CreateMap<Product, ProductListModel>();
            CreateMap<Product, ProductModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Order, OrderModel>();
        }
    }

}
