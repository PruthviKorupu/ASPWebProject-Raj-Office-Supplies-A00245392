using AutoMapper;
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
    public class ShopServiceTests
    {
        Mapper _mapper;
        Mock<IUnitOfWork> mockUOF;
        IShopService shopService;

        static List<Shop> expectedfullList = new List<Shop>()
        {
            new Shop()
            {
                Name = "RajOfficeSupplies",
                Description = "Lorem ipsum dolor sit amet",
                PicturePath = "https://drive.google.com/uc?id=1RgUcT1Y3Kog5g6zOVbYoM4FxTz6Nqxqni",
                Address = "address1",
            },
        };
        List<ShopEntity> shopEntities;

        [SetUp]
        public void SetUp()
        {
            _mapper = UnitTestsHelper.CreateMapperProfile();
            shopEntities = _mapper.Map<List<ShopEntity>>(expectedfullList);

            mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(x => x.ShopRepository.GetAll()).Returns(shopEntities);

            shopService = new ShopService(mockUOF.Object, _mapper);
        }


        [Test]
        public void GetAllShops_ListOfEntitiesPassed_EqualListOfDTOReturned()
        {
            //arrange

            //act
            var actual = shopService.GetAllShops().ToList();

            //assert
            Assert.True(actual != null);
            Assert.AreEqual(expectedfullList.Count, actual.Count);

            for (int i = 0; i < expectedfullList.Count; i++)
            {
                Assert.AreEqual(expectedfullList[0].Name, actual[0].Name);
            }
        }

        [Test]
        public void GetAllStocks_ListOfEntitiesPassed_IEnumerableExpected()
        {
            //arrange

            //act
            var actual = shopService.GetAllShops();

            //assert
            Assert.IsInstanceOf<IEnumerable<Shop>>(actual);
        }
    }
}
