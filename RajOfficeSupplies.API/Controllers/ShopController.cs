using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RajOfficeSuppliesAPI.Controllers
{
	[Route("api/Shop")]
	[ApiController]
	public class ShopController : ControllerBase
	{
		IShopService _shopService;

		public ShopController(IShopService shopService)
		{
			_shopService = shopService;
		}

		[HttpPost]
		public ActionResult<Shop> AddShop(Shop shop)
		{
			_shopService.AddShop(shop);
			return CreatedAtAction(nameof(AddShop), new { id = shop.Id }, shop);
		}

		[HttpGet("{id}")]
		public ActionResult<Shop> GetShop(int id)
		{
			return Ok(_shopService.GetShop(id));
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteShop(int id)
		{
			Shop shop = _shopService.GetShop(id);
			if (shop == null)
			{
				return BadRequest();
			}

			_shopService.DeleteShop(id);
			return NoContent();
		}
	}
}
