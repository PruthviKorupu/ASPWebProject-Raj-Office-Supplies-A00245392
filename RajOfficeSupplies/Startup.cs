using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Services;
using BLL.Interfaces;
using DAL;
using DAL.Entities;
using DAL.Repositories;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Stripe;
using System.IO;

namespace RajOfficeSupplies
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			string path = System.IO.Directory.GetCurrentDirectory();
			string projectPath = Path.GetFullPath(Path.Combine(path, @"..\"));


			services.AddDbContext<RajOfficeSuppliesDbContext>(opt =>
			{
				opt.UseLazyLoadingProxies();
				opt.UseSqlServer(Configuration.GetConnectionString("RajOfficeSuppliesDatabase").Replace("[DataDirectory]", projectPath));
			});

			services.AddScoped<IRepository<AddressEntity>, Repository<AddressEntity>>();
			services.AddScoped<IRepository<OrderEntity>, Repository<OrderEntity>>();
			services.AddScoped<IRepository<ProductCategoryEntity>, Repository<ProductCategoryEntity>>();
			services.AddScoped<IRepository<ProductEntity>, Repository<ProductEntity>>();
			services.AddScoped<IRepository<ShopEntity>, Repository<ShopEntity>>();
			services.AddScoped<IRepository<StockEntity>, Repository<StockEntity>>();
			services.AddScoped<IRepository<CartOrderItemEntity>, Repository<CartOrderItemEntity>>();
			services.AddScoped<IRepository<OrderStatusEntity>, Repository<OrderStatusEntity>>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddSingleton(new MapperConfiguration(c => c.AddProfile(new BLL.CustomMapper())).CreateMapper());
			services.AddTransient<IShopService, ShopService>();
			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<ICartService, CartService>();
			services.AddTransient<IStockService, StockService>();
			services.AddTransient<IOrderService, BLL.Services.OrderService>();

			services.AddIdentity<UserEntity, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
					.AddEntityFrameworkStores<RajOfficeSuppliesDbContext>()
					.AddDefaultTokenProviders();
			
			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			services.AddControllersWithViews().AddNewtonsoftJson();	
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			StripeConfiguration.ApiKey = "STRIPE";

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			
			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
