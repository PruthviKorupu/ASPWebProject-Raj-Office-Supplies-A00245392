using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RajOfficeSupplies.Models
{
	public class StockModel
	{
		public Stock Stock;
		public int Quantity = 1;

		public StockModel(Stock stock)
		{
			Stock = stock;
		}
	}
}
