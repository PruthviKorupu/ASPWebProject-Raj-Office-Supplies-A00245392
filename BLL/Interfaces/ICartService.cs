using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
	public interface ICartService
	{
		void AddItemToCart(Stock stock, int quantity, string cartId);
		void RemoveItemFromCart(int itemId, int stockId);
		void DeleteItem(int itemId, int stockId);
		List<CartOrderItem> GetAllItemsFromCart(string cartId);
		void UpdateCartId(string oldId, string newId);
		void SetOrderId(string cartId, int orderId);
		decimal CountTotalAmount(string cartId);
		CartOrderItem GetItemById(int id);
	}
}
