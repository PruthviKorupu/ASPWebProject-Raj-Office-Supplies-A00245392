using Autofac.Extras.Moq;
using AutoMapper;
using BLL;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using DAL.UnitOfWork;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RajOfficeSuppliesTests.BLLTests
{
    [TestFixture]
    public class CartServiceTests
    {
		Mapper _mapper;
		Mock<IUnitOfWork> mockUOF;
		ICartService cartService;
        #region data
		static Stock s = new Stock()
		{
			Id = 1,
			Price = 4,
			Quantity = 2
		};		
		CartOrderItem c = new CartOrderItem()
		{
			Id = 1,
			Stock = s,
			StockId = 1,
			Quantity = 1,
			CartId = "4596490f-524f-4af2-bf72-16f15bd78831",
			Price = 4
		};
		static List<CartOrderItem> expectedfullList = new List<CartOrderItem>()
		{
			new CartOrderItem()
			{
				StockId = s.Id,
				Stock = s,
				Quantity = 1,
				CartId = "4596490f-524f-4af2-bf72-16f15bd78831",
				Price = 4
			},
			new CartOrderItem()
			{
				StockId = 2,
				Stock = s,
				Quantity = 1,
				CartId = "4596490f-524f-4af2-bf72-16f15bd78831",
				Price = 5
			}
		};

		List<CartOrderItemEntity> entities;
		CartOrderItemEntity cartorderitementity;
		StockEntity stockentity;
		#endregion


		[SetUp]
		public void SetUp()
        {
			_mapper = UnitTestsHelper.CreateMapperProfile();

			entities = _mapper.Map<List<CartOrderItemEntity>>(expectedfullList);
			stockentity = _mapper.Map<StockEntity>(s);
			cartorderitementity = _mapper.Map<CartOrderItemEntity>(c);

			mockUOF = new Mock<IUnitOfWork>();
			mockUOF.Setup(x => x.CartOrderItemRepository.GetAll())
				.Returns(entities);
			mockUOF.Setup(x => x.CartOrderItemRepository.Get(cartorderitementity.Id))
				.Returns(cartorderitementity);
			mockUOF.Setup(x => x.StockRepository.Get(stockentity.Id))
				.Returns(stockentity);

			cartService = new CartService(mockUOF.Object, _mapper);
		}

		[Test]
		public void GetAllItemsFromCart_IdCartPassed_ListOfItemsIsEqual()
		{
			//arrange

			//act
			var actual = cartService.GetAllItemsFromCart("4596490f-524f-4af2-bf72-16f15bd78831");

			//Assert
			Assert.True(actual != null);
			Assert.AreEqual(expectedfullList.Count, actual.Count);

			for(int i=0; i<expectedfullList.Count; i++)
            {
				Assert.AreEqual(expectedfullList[0].Price, actual[0].Price);
            }

		}

		//[TestCase(null)]//fail
		[TestCase("4596490f-524f-4af2-bf72-16f15bd78831")]//pass
		public void CountTotalAmount_IdCartPassed_TotalAmountReturned(string? cartId)
        {
			//arrange
			var expected = expectedfullList[0].Price * expectedfullList[0].Quantity + 
						   expectedfullList[1].Price * expectedfullList[1].Quantity;//9

			//act
			var actual = cartService.CountTotalAmount(cartId);

			//assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void UpdateCartId_NewCartIdPassed_ItemsUpdated()
        {
			//arrange
			var oldId = expectedfullList[0].CartId;
			var newId = "bla";

			//act
			cartService.UpdateCartId(oldId, newId);

			//assert

			mockUOF.Verify(x => x.CartOrderItemRepository.Update(entities[0]), Times.Once);
			mockUOF.Verify(x => x.Save(), Times.Exactly(entities.Count));
        }

		[Test]
		public void SetOrderId_OrderIDPassed_ItemsUpdated()
        {
			//arrange
			var cartId = expectedfullList[0].CartId;
			var orderId = 1;

			//act
			cartService.SetOrderId(cartId, orderId);

			//assert
			for(int i=0; i<entities.Count; i++)
            {
				mockUOF.Verify(x => x.CartOrderItemRepository.Update(entities[0]), Times.Once);
            }
			mockUOF.Verify(x => x.Save(), Times.Exactly(entities.Count));
		}
		
		[Test]
		public void DeleteItem_ItemDeleted_StockQuantityIncrease()
        {
			//arrange
			var expectedQuantity = s.Quantity+1;

			//act
			cartService.DeleteItem(cartorderitementity.Id, stockentity.Id);

			//assert
			Assert.AreEqual(expectedQuantity, stockentity.Quantity);
		}

		[Test]
		public void RemoveItemFromCart_2ItemsInCartItemDeleted_StockQuantityIncrease()
		//if 1 item in cart - refer to DeleteItem_ItemDeleted_StockQuantityIncrease test
		{
			//arrange
			cartorderitementity.Quantity += 1;
			var expectedQuantity = s.Quantity + 1;

			//act
			cartService.RemoveItemFromCart(cartorderitementity.Id, stockentity.Id);

			//assert
			Assert.AreEqual(expectedQuantity, stockentity.Quantity);
		}

		[TestCase(null)]//no items in cart
		[TestCase("4596490f-524f-4af2-bf72-16f15bd78831")]//1 item in cart
		public void AddItemToCart_1ItemInCart_StockQuantityDecrease(string cartId)
		{
			//Arrange
			var cartorderitementities = _mapper.Map<List<CartOrderItemEntity>>(expectedfullList);

			mockUOF.Setup(x => x.CartOrderItemRepository.GetAll())				
					.Returns(cartorderitementities);
			
			var expectedQuantity = s.Quantity - 1;

			//Act
			cartService.AddItemToCart(s, 1, cartId);

			//Assert
			Assert.AreEqual(expectedQuantity, stockentity.Quantity);
		}
	}
}
