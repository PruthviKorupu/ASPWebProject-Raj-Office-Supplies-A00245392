using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly RajOfficeSuppliesDbContext _context;
		public IRepository<AddressEntity> AddressRepository { get; }
		public IRepository<OrderEntity> OrderRepository { get; }
		public IRepository<ProductCategoryEntity> ProductCategoryRepository { get; }
		public IRepository<ProductEntity> ProductRepository { get; }
		public IRepository<ShopEntity> ShopRepository { get; }
		public IRepository<CartOrderItemEntity> CartOrderItemRepository { get; }
		public IRepository<StockEntity> StockRepository { get; }
		public IRepository<OrderStatusEntity> OrderStatusRepository { get; }



		public UnitOfWork(RajOfficeSuppliesDbContext context,
			IRepository<AddressEntity> addresses, IRepository<OrderEntity> orders,
			IRepository<ProductEntity> products, IRepository<ShopEntity> shops,
			IRepository<CartOrderItemEntity> cartOrderItems,
			IRepository<StockEntity> stocks, IRepository<ProductCategoryEntity> categories, 
			IRepository<OrderStatusEntity> orderStatusRepository)
		{
			_context = context;
			AddressRepository = addresses;
			OrderRepository = orders;
			ProductRepository = products;
			ShopRepository = shops;
			CartOrderItemRepository = cartOrderItems;
			StockRepository = stocks;
			ProductCategoryRepository = categories;
			OrderStatusRepository = orderStatusRepository;
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
