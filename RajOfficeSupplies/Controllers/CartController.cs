using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RajOfficeSupplies.Models;

namespace RajOfficeSupplies.Controllers
{
	public class CartController : Controller
	{
		IStockService _stockService;
		ICartService _cartService;
		private readonly UserManager<UserEntity> _userManager;
		public const string CartSessionKey = "CartId";

		public CartController(ICartService cartService,
			IStockService stockService, UserManager<UserEntity> userManager)
		{
			_cartService = cartService;
			_stockService = stockService;
			_userManager = userManager;
		}

		public IActionResult AddItemToCartFromProductPage(int stockId, int quantity)
		{
			Stock stock = _stockService.GetStockById(stockId);

			if (quantity <= stock.Quantity)
			{
				_cartService.AddItemToCart(stock, quantity, GetCartId().Result);
			}

			return RedirectToAction("ShowProduct", "Product", new { stockId });
		}

		public IActionResult AddItemToCartByPlus(int stockId)
		{
			Stock stock = _stockService.GetStockById(stockId);

			if (stock.Quantity >= 1)
			{
				_cartService.AddItemToCart(stock, 1, GetCartId().Result);
			}

			return RedirectToAction("Cart", "Cart");
		}

		public IActionResult RemoveItemFromCartByMinus(int itemId, int stockId)
		{
			_cartService.RemoveItemFromCart(itemId, stockId);
			return RedirectToAction("Cart", "Cart");
		}

		public IActionResult RemoveItemFromCartByX(int itemId, int stockId)
		{
			_cartService.DeleteItem(itemId, stockId);
			return RedirectToAction("Cart", "Cart");
		}

		public IActionResult Cart()
		{
			CartModel model = new CartModel(_cartService.GetAllItemsFromCart(GetCartId().Result));
			return View(model);
		}

		public async Task<string> GetCartId()
		{
			if (HttpContext.User.Identity.IsAuthenticated)
			{
				var user = await _userManager.GetUserAsync(User);

				return user.Email;
			}

			else if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(CartSessionKey)))
			{
				Guid tempCartId = Guid.NewGuid();
				HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
			}

			return HttpContext.Session.GetString(CartSessionKey);
		}
	}
}
