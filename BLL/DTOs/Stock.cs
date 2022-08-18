using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
	public class Stock : BaseDTO
	{
		private Shop _shop;
		private Product _product;
		private decimal _price;
		private int _quantity;

		public Shop Shop { get => _shop; set =>  _shop = value; }
		public Product Product { get => _product; set => _product = value; }		
		public decimal Price { get => _price; set => _price = value; }
		public int Quantity { get => _quantity; set => _quantity = value; }

		
		//50% discount to an Product in Stock
		//public static Stock operator --(Stock c1)
		//{
		//	return new Stock { Price = c1.Price / 2 };
		//}
	}
}
