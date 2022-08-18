using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
	public interface IShopService
	{
		IEnumerable<Shop> GetAllShops();
		void AddShop(Shop shop);
		Shop GetShop(int id);
		void DeleteShop(int id);
	}
}
