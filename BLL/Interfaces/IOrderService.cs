using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
	public interface IOrderService
	{
		int AddOrderAndAddress(Order order);
		//decimal CountTotalAmount(Order order);
		Order GetOrderById(int orderId);
		void AddPaymentToOrder(string id, int orderId);
		void ChangeOrderStatus(int orderId, int orderStatusId);
		int GetOrderStatusByName(string name);
		IEnumerable<Order> GetAllOrdersFromEmail(string email);
		IEnumerable<CartOrderItem> GetAllOrderItems(int orderId);
	}
}
