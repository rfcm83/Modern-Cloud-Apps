using Contoso.Apps.SportsLeague.Admin.Models;
using Contoso.Apps.SportsLeague.Data.Logic;
using Contoso.Apps.SportsLeague.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace Contoso.Apps.SportsLeague.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ProductContext context)
        {
            _db = context;
        }

        private readonly ProductContext _db;

        //[Authorize(AuthenticationSchemes = "EasyAuth")]
        public ActionResult Index()
        {
            //var orderId = 2;
            //var order = new Order();
            List<Order> orders = new List<Order>();
            var orderActions = new OrderActions(_db);

            //order = orderActions.GetOrder(orderId);
            orders = orderActions.GetCompletedOrders();


            var vm = new Models.HomeModel
            {
                DisplayName = base.DisplayName,
                Orders = orders
            };

            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity.Name;
            }

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var vm = new Models.BaseModel
            {
                DisplayName = base.DisplayName
            };

            return View(vm);
        }

        public ActionResult Details(int Id)
        {
            var order = new Order();
            var orderActions = new OrderActions(_db);

            order = orderActions.GetOrder(Id);

            var vm = new Models.DetailsModel
            {
                DisplayName = base.DisplayName,
                Order = order
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}