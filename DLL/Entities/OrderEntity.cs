using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	[Table("Orders")]
	public class OrderEntity : BaseEntity
	{
		private string _email;
		private DateTime _date;
		private ICollection<CartOrderItemEntity> _cartOrderItems;
		private decimal _totalAmount;
		private string _paymentIntentId;
		private OrderStatusEntity _orderStatus;
		private int _orderStatusId;
		private AddressEntity _address;
		
		[Required]
		public string Email { get => _email; set => _email = value; }
		[Required]
		public DateTime Date { get => _date; set => _date = value; }
		[Required]
		public virtual OrderStatusEntity OrderStatus { get => _orderStatus; set => _orderStatus = value; }
		[Required]
		public virtual AddressEntity Address { get => _address; set => _address = value; }
		public decimal TotalAmount { get => _totalAmount; set => _totalAmount = value; }
		public string PaymentIntentId { get => _paymentIntentId; set => _paymentIntentId = value; }
		public int OrderStatusId { get => _orderStatusId; set => _orderStatusId = value; }
		public virtual ICollection<CartOrderItemEntity> CartOrderItems { get => _cartOrderItems; set => _cartOrderItems = value; }

		public OrderEntity()
		{
			CartOrderItems = new HashSet<CartOrderItemEntity>();
		}
	}
}
