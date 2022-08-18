using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
	public class CustomMapper : Profile
	{
        public CustomMapper()
        {
            CreateMap<Address, AddressEntity>()
                .ReverseMap();
            CreateMap<CartOrderItem, CartOrderItemEntity>()
                .ReverseMap();
            CreateMap<Order, OrderEntity>()
                .ForMember(d => d.CartOrderItems, opt => opt.MapFrom(src => src.CartOrderItems))
                .ReverseMap();
            CreateMap<ProductCategory, ProductCategoryEntity>()
                .ReverseMap();
            CreateMap<Product, ProductEntity>()
                .ReverseMap();
            CreateMap<Shop, ShopEntity>()
                .ReverseMap();
            CreateMap<Stock, StockEntity>()
                .ReverseMap();
            CreateMap<OrderStatus, OrderStatusEntity>()
                .ReverseMap();
        }
    }
}
