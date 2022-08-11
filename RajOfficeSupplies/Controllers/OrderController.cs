using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RajOfficeSupplies.Models;
using Address = BLL.DTOs.Address;
using Order = BLL.DTOs.Order;

namespace RajOfficeSupplies.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		ICartService _cartService;
		IOrderService _orderService;
		private readonly UserManager<UserEntity> _userManager;

		public OrderController(ICartService cartService, IOrderService orderService, 
			UserManager<UserEntity> userManager)
		{
			_cartService = cartService;
			_orderService = orderService;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult CreateOrder()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> CreateOrder(CreateOrderModel model)
		{
			if (ModelState.IsValid)
			{
				//address is created
				Address address = new Address()
				{
					FullName = model.FullName,
					Street = model.Street,
					City = model.City,
					Zip = model.Zip
				};

				var user = await _userManager.GetUserAsync(User);

				//order with pending status is created
				Order order = new Order()
				{
					Email = user.Email,
					Address = address,
					TotalAmount = _cartService.CountTotalAmount(user.Email),
					//Items = _cartService.GetAllItemsFromCart(user.Email),
					OrderStatusId = _orderService.GetOrderStatusByName("Pending")
				};

				int orderId = _orderService.AddOrderAndAddress(order);
				_cartService.SetOrderId(user.Email, orderId);

				return RedirectToAction("Payment", "Order", new { orderId });
			}

			return View(model);
		}

		public IActionResult Payment(int orderId)
		{
			return View(_orderService.GetOrderById(orderId));
		}
		
		public IActionResult PaymentSucceeded(string paymentIntentId, string orderId)
		{
			_orderService.AddPaymentToOrder(paymentIntentId, int.Parse(orderId));
;
			_cartService.UpdateCartId(_orderService.GetOrderById(int.Parse(orderId)).Email, "");

			return View();
		}

		public IActionResult PaymentFailed(string orderId)
		{
			_orderService.ChangeOrderStatus(int.Parse(orderId), _orderService.GetOrderStatusByName("Payment failed"));

			return View();
		}

		public async Task<IActionResult> Orders()
		{
			var user = await _userManager.GetUserAsync(User);

			List<Order> orders = _orderService.GetAllOrdersFromEmail(user.Email).ToList();
			return View(orders);
		}

		public IActionResult Order(int orderId)
		{
			Order order = _orderService.GetOrderById(orderId);
			return View(order);
		}
	}
}
