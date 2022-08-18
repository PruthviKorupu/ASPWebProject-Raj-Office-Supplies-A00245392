using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using Newtonsoft.Json;
using BLL.Interfaces;

namespace RajOfficeSupplies.Controllers
{
    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentIntentApiController : Controller
    {
        IOrderService _orderService;

		public PaymentIntentApiController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = (long)_orderService.GetOrderById(request.OrderId).TotalAmount*100,
                Currency = "uah",
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }

        public class PaymentIntentCreateRequest
        {
            [JsonProperty("orderId")]
            public int OrderId { get; set; }
        }
    }
}