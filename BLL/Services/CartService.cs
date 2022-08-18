using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
	public class CartService : ICartService
	{
		private readonly IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public CartService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public CartOrderItem GetItemById(int id)
		{
			return _mapper.Map<CartOrderItem>(_unitOfWork.CartOrderItemRepository.Get(id));
		}

		public void AddItemToCart(Stock stock, int quantity, string cartId)
		{
			CartOrderItemEntity itemEntity = _unitOfWork.CartOrderItemRepository.GetAll().ToList().Find(
						i => i.CartId == cartId && i.StockId == stock.Id);

			if (itemEntity != null)
			{
				itemEntity.Quantity += quantity;
				itemEntity.Price = itemEntity.Stock.Price * itemEntity.Quantity;
				_unitOfWork.CartOrderItemRepository.Update(itemEntity);
			}
			else
			{
				CartOrderItem newItem = new CartOrderItem()
				{
					StockId = stock.Id,
					Quantity = quantity,
					CartId = cartId,
					Price = stock.Price * quantity
				};

				CartOrderItemEntity newItemEntity = _mapper.Map<CartOrderItemEntity>(newItem);
				_unitOfWork.CartOrderItemRepository.Add(newItemEntity);
			}

			StockEntity stockEntity = _unitOfWork.StockRepository.Get(stock.Id);
			stockEntity.Quantity -= quantity;
			_unitOfWork.StockRepository.Update(stockEntity);

			_unitOfWork.Save();
		}

		public void RemoveItemFromCart(int itemId, int stockId)
		{
			CartOrderItemEntity itemEntity = _unitOfWork.CartOrderItemRepository.Get(itemId);

			if (itemEntity.Quantity > 1)
			{
				itemEntity.Quantity--;
				itemEntity.Price = itemEntity.Stock.Price * itemEntity.Quantity;
				_unitOfWork.CartOrderItemRepository.Update(itemEntity);
			}
			else if (itemEntity.Quantity == 1)
			{
				_unitOfWork.CartOrderItemRepository.Delete(itemEntity.Id);
			}

			StockEntity stockEntity = _unitOfWork.StockRepository.Get(stockId);
			stockEntity.Quantity++;
			_unitOfWork.StockRepository.Update(stockEntity);

			_unitOfWork.Save();
		}



		public void DeleteItem(int itemId, int stockId)
		{
			int quantity = _unitOfWork.CartOrderItemRepository.Get(itemId).Quantity;
			_unitOfWork.CartOrderItemRepository.Delete(itemId);

			StockEntity stockEntity = _unitOfWork.StockRepository.Get(stockId);
			stockEntity.Quantity += quantity;
			_unitOfWork.StockRepository.Update(stockEntity);

			_unitOfWork.Save();
		}


		public List<CartOrderItem> GetAllItemsFromCart(string cartId)
		{
			List<CartOrderItemEntity> sortedItemEntities = _unitOfWork.CartOrderItemRepository.GetAll()
				.ToList().FindAll(i => i.CartId == cartId);
			return _mapper.Map<List<CartOrderItem>>(sortedItemEntities);
		}
  
		public void UpdateCartId(string oldId, string newId)
		{
			List<CartOrderItemEntity> sortedItemEntities = _unitOfWork.CartOrderItemRepository.GetAll()
				.ToList().FindAll(i => i.CartId == oldId);
			if (sortedItemEntities.Any())
			{
				foreach (CartOrderItemEntity i in sortedItemEntities)
				{
					i.CartId = newId;
					_unitOfWork.CartOrderItemRepository.Update(i);
					_unitOfWork.Save();
				}
			}
		}

		public void SetOrderId(string cartId, int orderId)
		{
			List<CartOrderItemEntity> sortedItemEntities = _unitOfWork.CartOrderItemRepository.GetAll()
				.ToList().FindAll(i => i.CartId == cartId);
			if (sortedItemEntities.Any())
			{
				foreach (CartOrderItemEntity i in sortedItemEntities)
				{
					i.OrderId = orderId;
					_unitOfWork.CartOrderItemRepository.Update(i);
					_unitOfWork.Save();
				}
			}
		}

		public decimal CountTotalAmount(string cartId)
		{
			decimal totalAmount = 0;

			List<CartOrderItemEntity> sortedItemEntities = _unitOfWork.CartOrderItemRepository.GetAll()
				.ToList().FindAll(i => i.CartId == cartId);
			
			if (sortedItemEntities.Any())
			{
				foreach (CartOrderItemEntity i in sortedItemEntities)
				{
					totalAmount += i.Price;
				}
			}

			return totalAmount;
		}
	}
}
