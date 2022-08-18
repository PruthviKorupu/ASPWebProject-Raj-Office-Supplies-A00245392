using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	[Table("Stocks")]
	public class StockEntity : BaseEntity
	{
		private ShopEntity _shop;
		private ProductEntity _product;
		private decimal _price;
		private int _quantity;
		
		[Required]
		public virtual ShopEntity Shop { get => _shop; set => _shop = value; }
		[Required]
		public virtual ProductEntity Product { get => _product; set => _product = value; }
		[Required]
		[Range(0, int.MaxValue)]
		public int Quantity { get => _quantity; set => _quantity = value; }
		public decimal Price { get => _price; set => _price = value; }
	}
}
