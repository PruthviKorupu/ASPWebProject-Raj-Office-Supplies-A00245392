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
    [Route("api/Product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
        IStockService _stockService;

		public ProductController(IStockService stockService)
		{
			_stockService = stockService;
		}

		[HttpGet]
        public ActionResult<IEnumerable<StocksModel>> GetProducts()
        {
            return Ok(new StocksModel(_stockService.GetAllStocks().ToList()));
        }

        [HttpGet("{id}")]
        public ActionResult<StockModel> GetProduct(int id)
        {
            var stock = _stockService.GetStockById(id);
            if (stock == null)
            {
                return BadRequest();
            }
            return Ok(new StockModel(stock));
        }
    }
}
