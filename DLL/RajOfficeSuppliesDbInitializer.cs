using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
	public class RajOfficeSuppliesDbInitializer
	{
		private static bool initialized = false;
		public static void Initialize(RajOfficeSuppliesDbContext context)
		{
			if (initialized)
			{
				return;
			}

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			OrderStatusEntity pending = new OrderStatusEntity() { Name = "Pending" };
			OrderStatusEntity paymentReceived = new OrderStatusEntity() { Name = "Payment received" };
			OrderStatusEntity paymentFailed = new OrderStatusEntity() { Name = "Payment failed" };
			OrderStatusEntity sent = new OrderStatusEntity() { Name = "Sent" };
			OrderStatusEntity delivered = new OrderStatusEntity() { Name = "Delivered" };


			ProductCategoryEntity book = new ProductCategoryEntity() { Name = "Book" };
			ProductCategoryEntity pencil = new ProductCategoryEntity() { Name = "Pencil" };




			ProductEntity blamblamble = new ProductEntity()
			{
				Name = "Introduction To Data Structures",
				Description = "desc1",
				PicturePath = "https://drive.google.com/uc?id=1J1U9zCfBr5VFohWbsk1GuU7sdtBSkPFaYG",
				Category = book
			};

			ProductEntity cheese = new ProductEntity()
			{
				Name = "Nataraj",
				Description = "desc2",
				PicturePath = "https://drive.google.com/uc?id=1yUI_gdsGS0TZrpf_Dx7ricb8X9hJuY2gX3",
				Category = pencil
			};

			 
  
			 

			context.OrderStatuses.AddRange(new List<OrderStatusEntity> { pending, paymentReceived, paymentFailed, sent, delivered });
			
 
			context.SaveChanges();

			initialized = true;
		}
	}
}
