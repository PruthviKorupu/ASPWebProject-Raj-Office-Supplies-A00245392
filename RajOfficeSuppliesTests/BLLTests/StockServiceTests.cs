using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.UnitOfWork;
using Moq;
using NUnit.Framework;

namespace RajOfficeSuppliesTests.BLLTests
{
	[TestFixture]
	public class StockServiceTests
	{
		Mapper _mapper;
		Mock<IUnitOfWork> mockUOF;
		IStockService stockService;

		#region
		static List<Stock> expectedfullList = new List<Stock>()
		{
			new Stock()
			{
				Id = 1,
				Price = 4,
				Quantity = 2
			},
			new Stock()
			{
				Id = 2,
				Price = 6,
				Quantity = 6
			}
		};
		static Stock s = new Stock()
		{
			Id = 1,
			Price = 4,
			Quantity = 2
		};
		List<StockEntity> stockEntities;
		#endregion
	
		[SetUp]
		public void SetUp()
		{
			_mapper = UnitTestsHelper.CreateMapperProfile();
			stockEntities = _mapper.Map<List<StockEntity>>(expectedfullList);

			mockUOF = new Mock<IUnitOfWork>();
			mockUOF.Setup(x => x.StockRepository.GetAll()).Returns(stockEntities);

			stockService = new StockService(mockUOF.Object, _mapper);
		}

		[Test]
		public void GetAllStocks_ListOfEntitiesPassed_EqualListOfDTOReturned()
        {
			//arrange

			//act
			var actual = stockService.GetAllStocks().ToList();

			//assert
			Assert.True(actual != null);
			Assert.AreEqual(expectedfullList.Count, actual.Count);

			for (int i = 0; i < expectedfullList.Count; i++)
			{
				Assert.AreEqual(expectedfullList[0].Price, actual[0].Price);
			}
		}

		[Test]
		public void GetAllStocks_ListOfEntitiesPassed_IEnumerableExpected()
        {
			//arrange

			//act
			var actual = stockService.GetAllStocks();

			//assert
			Assert.IsInstanceOf<IEnumerable<Stock>>(actual);
		}

		[Test]
		public void GetStockById_StockIdPassed_StockDTOExpected()
        {
			//arrange
			var stockID = 1;
			var stockentity = _mapper.Map<StockEntity>(s);
			mockUOF.Setup(x => x.StockRepository.Get(stockID)).Returns(stockentity);

			//act
			var actual = stockService.GetStockById(stockID);

			//assert
			Assert.IsInstanceOf<Stock>(actual);
		}

		[Test]
		public void GetStockById_StockIdPassed_EqualStockDTOExpected()
        {
			//arrange
			var stockID = 1;
			var stockentity = _mapper.Map<StockEntity>(s);
			mockUOF.Setup(x => x.StockRepository.Get(stockID)).Returns(stockentity);

			//act
			var actual = stockService.GetStockById(stockID);

			//assert
			Assert.AreEqual(s.Id, actual.Id);
		}
	}
}
