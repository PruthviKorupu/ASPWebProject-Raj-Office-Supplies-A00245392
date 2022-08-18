using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UnitOfWork
{
	public interface IUnitOfWork
	{
		IRepository<AddressEntity> AddressRepository { get; }
		IRepository<OrderEntity> OrderRepository { get; }
		IRepository<ProductCategoryEntity> ProductCategoryRepository { get; }
		IRepository<ProductEntity> ProductRepository { get; }
		IRepository<ShopEntity> ShopRepository { get; }
		IRepository<CartOrderItemEntity> CartOrderItemRepository { get; }
		IRepository<StockEntity> StockRepository { get; }
		IRepository<OrderStatusEntity> OrderStatusRepository { get; }

		void Save();
	}
}
