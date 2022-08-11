using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using RajOfficeSuppliesAPI;

namespace IntegrationTests
{
    internal class WebAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                RemoveShopDbContextRegistration(services);

                var serviceProvider = GetInMemoryServiceProvider();

                services.AddDbContextPool<RajOfficeSuppliesDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.Empty.ToString());
                    options.UseInternalServiceProvider(serviceProvider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<RajOfficeSuppliesDbContext>();

                InitData(context);
            });
        }

        private void InitData(RajOfficeSuppliesDbContext context)
        {
			OrderStatusEntity pending = new OrderStatusEntity() { Name = "Pending" };
			OrderStatusEntity paymentReceived = new OrderStatusEntity() { Name = "Payment received" };
			OrderStatusEntity paymentFailed = new OrderStatusEntity() { Name = "Payment failed" };
			OrderStatusEntity sent = new OrderStatusEntity() { Name = "Sent" };
			OrderStatusEntity delivered = new OrderStatusEntity() { Name = "Delivered" };


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

			 
  
			 

			context.SaveChanges();
		}
        private static ServiceProvider GetInMemoryServiceProvider()
        {
            return new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
        }

        private static void RemoveShopDbContextRegistration(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<RajOfficeSuppliesDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }
    }
}