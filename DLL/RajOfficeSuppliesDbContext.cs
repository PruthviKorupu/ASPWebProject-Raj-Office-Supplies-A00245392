using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
	public class RajOfficeSuppliesDbContext : IdentityDbContext<UserEntity>
	{
		public DbSet<AddressEntity> Addresses { get; set; }
		public DbSet<OrderEntity> Orders { get; set; }
		public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
		public DbSet<ProductEntity> Products { get; set; }
		public DbSet<ShopEntity> Shops { get; set; }
		public DbSet<CartOrderItemEntity> CartOrderItems { get; set; }
		public DbSet<StockEntity> Stocks { get; set; }
		public DbSet<OrderStatusEntity> OrderStatuses { get; set; }


		public RajOfficeSuppliesDbContext(DbContextOptions<RajOfficeSuppliesDbContext> options) : base(options)
		{
			RajOfficeSuppliesDbInitializer.Initialize(this);
		}
	}
}
