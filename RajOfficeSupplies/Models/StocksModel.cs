using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RajOfficeSupplies.Models
{
	public class StocksModel
	{
		public List<Stock> Stocks;

		public StocksModel(List<Stock> stocks)
		{
			Stocks = stocks;
		}
	}
}
