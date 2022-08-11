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
	[Route("api/Home")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		IShopService _shopService;
		ICategoryService _categoryService;

		public HomeController(IShopService shopService, ICategoryService categoryService)
		{
			_shopService = shopService;
			_categoryService = categoryService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<HomePageModel>> GetShopsAndCategories()
		{
			return Ok(new HomePageModel(_shopService.GetAllShops().ToList(),
				_categoryService.GetAllCategories().ToList()));
		}
	}
}
