using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
	public class Order : BaseDTO
	{
		private string _email;
		private DateTime _date = DateTime.Now;
		private List<CartOrderItem> _cartOrderItems;
		private decimal _totalAmount;
		private string _paymentIntentId;
		private OrderStatus _orderStatus;
		private int _orderStatusId;
		private Address _address;

		//private decimal _itemsAmount;
		//private decimal _deliveryAmount;

		public string Email { get => _email; set => _email = value; }
		public DateTime Date { get => _date; set => _date = value; }
		public Address Address { get => _address; set => _address = value; }
		public decimal TotalAmount { get => _totalAmount; set => _totalAmount = value; }
		public string PaymentIntentId { get => _paymentIntentId; set => _paymentIntentId = value; }
		public int OrderStatusId { get => _orderStatusId; set => _orderStatusId = value; }
		public List<CartOrderItem> CartOrderItems { get => _cartOrderItems; set => _cartOrderItems = value; }
		public OrderStatus OrderStatus { get => _orderStatus; set => _orderStatus = value; }

		//public decimal ItemsAmount { get => _itemsAmount; set => _itemsAmount = value; }
		//public decimal DeliveryAmount { get => _deliveryAmount; set => _deliveryAmount = value; }


		//Count total amount consisting of delivery and item prices
		//public static Order operator +(Order c1, Order c2)
		//{
		//	return new Order { TotalAmount = c1.DeliveryAmount + c2.ItemsAmount };
		//}
	}
}
