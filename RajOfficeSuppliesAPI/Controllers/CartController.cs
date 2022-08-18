using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RajOfficeSupplies.Models;

namespace RajOfficeSuppliesAPI.Controllers
{
	[Route("api/Cart")]
	[ApiController]
	public class CartController : ControllerBase
	{
		ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		[HttpGet("{cartId}")]
		public ActionResult<CartModel> GetCart(string cartId)
		{
			return Ok(new CartModel(_cartService.GetAllItemsFromCart(cartId)));
		}

		[HttpDelete("{itemId}")]
		public ActionResult DeleteItem(int itemId)
		{
			CartOrderItem item = _cartService.GetItemById(itemId);
			if (item == null)
			{
				return BadRequest();
			}

			_cartService.RemoveItemFromCart(itemId, item.StockId);
			return NoContent();
		}
	}
}
