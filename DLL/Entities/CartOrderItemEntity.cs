using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	[Table("CartOrderItems")]
	public class CartOrderItemEntity : BaseEntity
	{

		private decimal _price;
		private int _quantity;
		private string _cartId;
		private StockEntity _stock;
		private int _stockId;
		private OrderEntity _order;
		private int? _orderId;

		public decimal Price { get => _price; set => _price = value; }
		[Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
		public int Quantity { get => _quantity; set => _quantity = value; }
		[Required]
		public string CartId { get => _cartId; set => _cartId = value; }
		public virtual StockEntity Stock { get => _stock; set => _stock = value; }
		public int StockId { get => _stockId; set => _stockId = value; }
		//[ForeignKey("OrderEntity")]
		public int? OrderId { get => _orderId; set => _orderId = value; }
		public virtual OrderEntity Order { get => _order; set => _order = value; }
	}
}
