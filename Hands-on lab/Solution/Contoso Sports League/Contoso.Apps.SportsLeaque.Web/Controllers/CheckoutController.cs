using AutoMapper;
using Contoso.Apps.Common;
using Contoso.Apps.SportsLeague.Data.Logic;
using Contoso.Apps.SportsLeague.Data.Models;
using Contoso.Apps.SportsLeague.Web.Helpers;
using Contoso.Apps.SportsLeague.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Contoso.Apps.SportsLeague.Web.Controllers
{
    public class CheckoutController : Controller
    {
        public CheckoutController(ProductContext context, IMapper mapper, IConfiguration config)
        {
            _db = context;
            _mapper = mapper;
            _config = config;
        }

        private readonly ProductContext _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        private string CartId
        {
            get
            {
                return new Helpers.CartHelpers().GetCartId(HttpContext);
            }
        }

        // GET: Checkout
        public ActionResult Index()
        {
            var vm = new CheckoutModel();
            var usersShoppingCart = new ShoppingCartActions(_db, CartId);
            
            var shoppingCartItems = usersShoppingCart.GetCartItems();
            var cartItemsVM = _mapper.Map<List<CartItemModel>>(shoppingCartItems);
            vm.CartItems = cartItemsVM;
            vm.OrderTotal = usersShoppingCart.GetTotal();
            vm.ItemsTotal = usersShoppingCart.GetCount();
        
            // To make filling out the form faster for demo purposes, pre-fill the values:
            vm.Order = new OrderModel
            {
                // Important! Keep this property here!
                Total = vm.OrderTotal,
                // Prefill properties for convenience:
                FirstName = "Bob",
                LastName = "Loblaw",
                Address = "1313 Mockingbird Lane",
                City = "Virginia Beach",
                State = "VA",
                PostalCode = "23456",
                Country = "United States",
                Email = "bobloblaw@contososportsleague.com",
                NameOnCard = "Bob Loblaw",
                CreditCardType = "Visa",
                CreditCardNumber = "4111111111111111",
                ExpirationDate = "12/20",
                CCV = "987",
                SMSOptIn = true
            };

            return View(vm);
        }

        // POST: Checkout/Start
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Review(CheckoutModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var gatewayCaller = new NVPAPICaller(_config, HttpContext);

                    var token = string.Empty;
                    var decoder = new NVPCodec();

                    // Call the gateway payment authorization API:
                    bool ret = gatewayCaller.DoCheckoutAuth(data.Order, ref token, ref decoder);

                    // If authorizaton is successful:
                    if (ret)
                    {
                        // Hydrate a new Order model from our OrderModel.
                        var myOrder = _mapper.Map<Data.Models.Order>(data.Order);
                        // Timestamp with a UTC date.
                        myOrder.OrderDate = DateTime.UtcNow;

                        // Add order to DB.
                        _db.Orders.Add(myOrder);
                        await _db.SaveChangesAsync();

                        // Get the shopping cart items and process them.
                        var usersShoppingCart = new ShoppingCartActions(_db, CartId);
                        
                        List<CartItem> myOrderList = usersShoppingCart.GetCartItems();

                        // Add OrderDetail information to the DB for each product purchased.
                        for (int i = 0; i < myOrderList.Count; i++)
                        {
                            // Create a new OrderDetail object.
                            var myOrderDetail = new OrderDetail();
                            myOrderDetail.OrderId = myOrder.OrderId;
                            myOrderDetail.ProductId = myOrderList[i].ProductId;
                            myOrderDetail.Quantity = myOrderList[i].Quantity;
                            myOrderDetail.UnitPrice = myOrderList[i].Product.UnitPrice;

                            // Add OrderDetail to DB.
                            _db.OrderDetails.Add(myOrderDetail);
                            _db.SaveChanges();
                        }

                        // Set OrderId.
                        HttpContext.Session.SetInt32("currentOrderId", myOrder.OrderId);
                        HttpContext.Session.SetString("Token", token);

                        // Report successful event to Application Insights.
                        var eventProperties = new Dictionary<string, string>();
                        eventProperties.Add("CustomerEmail", data.Order.Email);
                        eventProperties.Add("NumberOfItems", myOrderList.Count.ToString());
                        eventProperties.Add("OrderTotal", data.Order.Total.ToString("C2"));
                        eventProperties.Add("OrderId", myOrder.OrderId.ToString());
                        TelemetryHelper.TrackEvent("SuccessfulPaymentAuth", eventProperties);

                        data.Order.OrderId = myOrder.OrderId;
                        if (data.Order.CreditCardNumber.Length > 4)
                        {
                            // Only show the last 4 digits of the credit card number:
                            data.Order.CreditCardNumber = "xxxxxxxxxxx" + data.Order.CreditCardNumber.Substring(data.Order.CreditCardNumber.Length - 4);
                        }

                    }
                    else
                    {
                        var error = gatewayCaller.PopulateGatewayErrorModel(decoder);

                        // Report failed event to Application Insights.
                        Exception ex = new Exception(error.ToString());
                        ex.Source = "Contoso.Apps.SportsLeague.Web.CheckoutController.cs";
                        TelemetryHelper.TrackException(ex);

                        // Redirect to the checkout error view:
                        return RedirectToAction("Error", error);
                    }
                }
                catch (WebException wex)
                {
                    ExceptionUtility.LogException(wex, "CheckoutController.cs Complete Action");

                    var error = new CheckoutErrorModel
                    {
                        ErrorCode = wex.Message
                    };

                    if (wex.Response != null && wex.Response.GetType() == typeof(HttpWebResponse))
                    {
                        // Extract the response body from the WebException's HttpWebResponse:
                        error.LongMessage = ((HttpWebResponse)wex.Response).StatusDescription;
                    }

                    // Redirect to the checkout error view:
                    return RedirectToAction("Error", error);
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "CheckoutController.cs Review Action");

                    var error = new CheckoutErrorModel
                    {
                        ErrorCode = ex.Message
                    };

                    // Redirect to the checkout error view:
                    return RedirectToAction("Error", error);
                }
            }

            return View(data);
        }

        // POST: Checkout/Complete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Complete(OrderModel order)
        {
            try
            {
                // TODO: Complete the payment processing via the gateway and update the order...
                var gatewayCaller = new NVPAPICaller(_config, HttpContext);
                var finalPaymentAmount = string.Empty;
                var decoder = new NVPCodec();

                var token = HttpContext.Session.GetString("token");
                //PayerID = Session["payerId"].ToString();
                //finalPaymentAmount = Session["payment_amt"].ToString();
                finalPaymentAmount = order.Total.ToString("C2");

                var ret = gatewayCaller.DoCheckoutPayment(finalPaymentAmount, token, ref decoder);
                if (ret)
                {
                    // Retrieve PayPal confirmation value.
                    string PaymentConfirmation = decoder[NVPProperties.Properties.TRANSACTIONID].ToString();
                    order.PaymentTransactionId = PaymentConfirmation;

                    // Get the current order id.
                    int currentOrderId = -1;
                    if (HttpContext.Session.GetInt32("currentOrderId") != null && HttpContext.Session.GetInt32("currentOrderId")?.ToString() != string.Empty)
                    {
                        currentOrderId = Convert.ToInt32(HttpContext.Session.GetInt32("currentOrderId"));
                    }
                    Order myCurrentOrder;
                    if (currentOrderId >= 0)
                    {
                        // Get the order based on order id.
                        myCurrentOrder = _db.Orders.Single(o => o.OrderId == currentOrderId);
                        // Update the order to reflect payment has been completed.
                        myCurrentOrder.PaymentTransactionId = PaymentConfirmation;
                        // Save to DB.
                        await _db.SaveChangesAsync();

                        // Queue up a receipt generation request, asynchronously.
                        await new AzureQueueHelper(_config).QueueReceiptRequest(myCurrentOrder);

                        // Report successful event to Application Insights.
                        var eventProperties = new Dictionary<string, string>();
                        eventProperties.Add("CustomerEmail", myCurrentOrder.Email);
                        eventProperties.Add("OrderTotal", finalPaymentAmount);
                        eventProperties.Add("PaymentTransactionId", PaymentConfirmation);
                        TelemetryHelper.TrackEvent("OrderCompleted", eventProperties);
                    }

                    // Clear shopping cart.
                    var usersShoppingCart = new ShoppingCartActions(_db, CartId);
                    
                    await usersShoppingCart.EmptyCart();
                    

                    // Clear order id.
                    HttpContext.Session.Remove("currentOrderId");
                }
                else
                {
                    var error = gatewayCaller.PopulateGatewayErrorModel(decoder);

                    // Report failed event to Application Insights.
                    Exception ex = new Exception(error.ToString());
                    ex.Source = "Contoso.Apps.SportsLeague.Web.CheckoutController.cs";
                    TelemetryHelper.TrackException(ex);

                    // Redirect to the checkout error view:
                    return RedirectToAction("Error", error);
                }
            }
            catch (WebException wex)
            {
                ExceptionUtility.LogException(wex, "CheckoutController.cs Complete Action");

                var error = new CheckoutErrorModel
                {
                    ErrorCode = wex.Message
                };

                if (wex.Response != null && wex.Response.GetType() == typeof(HttpWebResponse))
                {
                    // Extract the response body from the WebException's HttpWebResponse:
                    error.LongMessage = ((HttpWebResponse)wex.Response).StatusDescription;
                }

                // Redirect to the checkout error view:
                return RedirectToAction("Error", error);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "CheckoutController.cs Complete Action");

                var error = new CheckoutErrorModel
                {
                    ErrorCode = ex.Message
                };

                // Redirect to the checkout error view:
                return RedirectToAction("Error", error);
            }

            return View(order);
        }

        public ActionResult Error(CheckoutErrorModel error)
        {
            return View(error);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
