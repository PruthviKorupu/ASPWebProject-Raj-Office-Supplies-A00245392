using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RajOfficeSupplies.Models
{
	public class HomePageModel
	{
		public List<Shop> Shops;
		public List<ProductCategory> Categories;
		public Shop Shop;
		public ProductCategory Category;

		public HomePageModel(List<Shop> shops, List<ProductCategory> categories)
		{
			Shops = shops;
			Categories = categories;
		}

	}
}
