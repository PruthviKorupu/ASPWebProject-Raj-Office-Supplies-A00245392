using AutoMapper;
using BLL;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
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
    public class CategoryServiceTests
    {
        Mapper _mapper;
        Mock<IUnitOfWork> mockUOF;
        ICategoryService categoryService;
        #region
        static List<ProductCategory> expectedfullList = new List<ProductCategory>()
		{
			new ProductCategory()
			{
				Name = "cat"
			},
			new ProductCategory()
			{
				Name = "egory"
			}
		};
		List<ProductCategoryEntity> categoryEntities;
		#endregion

		[SetUp]
		public void SetUp()
		{
			_mapper = UnitTestsHelper.CreateMapperProfile();
			categoryEntities = _mapper.Map<List<ProductCategoryEntity>>(expectedfullList);

			mockUOF = new Mock<IUnitOfWork>();
			mockUOF.Setup(x => x.ProductCategoryRepository.GetAll()).Returns(categoryEntities);

			categoryService = new CategoryService(mockUOF.Object, _mapper);
		}

		[Test]
		public void GetAllCategories_ListOfEntitiesPassed_EqualListOfDTOReturned()
		{
			//arrange

			//act
			var actual = categoryService.GetAllCategories().ToList();

			//assert
			Assert.True(actual != null);
			Assert.AreEqual(expectedfullList.Count, actual.Count);

			for (int i = 0; i < expectedfullList.Count; i++)
			{
				Assert.AreEqual(expectedfullList[i].Name, actual[i].Name);
			}
		}

		[Test]
		public void GetAllStocks_ListOfEntitiesPassed_IEnumerableExpected()
		{
			//arrange

			//act
			var actual = categoryService.GetAllCategories();

			//assert
			Assert.IsInstanceOf<IEnumerable<ProductCategory>>(actual);
		}
	}
}
