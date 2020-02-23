using Contoso.Apps.SportsLeague.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Contoso.Apps.SportsLeague.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        private readonly IConfiguration _config;

        public ActionResult Index() {
            //var orderId = 1;
            //var order = new Order();
            //using (var orderActions = new OrderActions(orderId))
            //{
            //    order = orderActions.GetOrder();
            //}

            var vm = new HomeModel();

            ViewBag.offersAPIUrl = _config.GetValue<string>("offersAPIUrl");

            return View(vm);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}