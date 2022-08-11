using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RajOfficeSupplies.Models;

namespace RajOfficeSupplies.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		IShopService _shopService;
		ICategoryService _categoryService;

		public HomeController(ILogger<HomeController> logger,
			IShopService shopService, ICategoryService categoryService)
		{
			_logger = logger;
			_shopService = shopService;
			_categoryService = categoryService;
		}

		[HttpGet]
		public IActionResult Index()
		{

			HomePageModel model = new HomePageModel(_shopService.GetAllShops().ToList(),
				_categoryService.GetAllCategories().ToList());
			return View(model);
		}

		public IActionResult ExampleProduct()
		{
			return View();
		}

		public IActionResult ExampleShop()
		{
			var model = new HomePageModel(_shopService.GetAllShops().ToList(),
				_categoryService.GetAllCategories().ToList());
			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
