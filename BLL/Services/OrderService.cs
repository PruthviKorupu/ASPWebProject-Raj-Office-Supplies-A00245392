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
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public int AddOrderAndAddress(Order order)
		{
			//order.TotalAmount = CountTotalAmount(order);
			OrderEntity orderEntity = _mapper.Map<OrderEntity>(order);
			_unitOfWork.OrderRepository.Add(orderEntity);

			_unitOfWork.Save();

			return orderEntity.Id;
		}

		//public decimal CountTotalAmount(Order order)
		//{
		//	decimal totalAmount = 0;

		//	foreach (CartOrderItem i in order.CartOrderItems)
		//	{
		//		totalAmount += i.Price;
		//	}

		//	return totalAmount;
		//}

		public Order GetOrderById(int orderId)
		{
			return _mapper.Map<Order>(_unitOfWork.OrderRepository.Get(orderId));
		}

		public void AddPaymentToOrder(string id, int orderId)
		{
			OrderEntity orderEntity = _unitOfWork.OrderRepository.Get(orderId);
			orderEntity.PaymentIntentId = id;
			orderEntity.OrderStatusId = GetOrderStatusByName("Payment received");
			_unitOfWork.OrderRepository.Update(orderEntity);
			_unitOfWork.Save();
		}

		//this one is for shop owner
		public void ChangeOrderStatus(int orderId, int orderStatusId)
		{
			OrderEntity orderEntity = _unitOfWork.OrderRepository.Get(orderId);
			orderEntity.OrderStatusId = orderStatusId;
			_unitOfWork.OrderRepository.Update(orderEntity);
			_unitOfWork.Save();
		}

		public int GetOrderStatusByName(string name)
		{
			return _unitOfWork.OrderStatusRepository.GetAll().ToList().Find(s => s.Name == name).Id;
		}

		public IEnumerable<Order> GetAllOrdersFromEmail(string email)
		{
			return _mapper.Map<IEnumerable<Order>>(_unitOfWork.OrderRepository.GetAll().ToList().FindAll(o => o.Email == email));
		}

		public IEnumerable<CartOrderItem> GetAllOrderItems(int orderId)
		{
			return _mapper.Map<IEnumerable<CartOrderItem>>(_unitOfWork.OrderRepository.Get(orderId).CartOrderItems);
		}
	}
}
