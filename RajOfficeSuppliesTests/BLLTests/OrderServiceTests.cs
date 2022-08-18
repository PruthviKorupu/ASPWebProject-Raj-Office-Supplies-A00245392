using AutoMapper;
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
    public class OrderServiceTests
    {
        Mapper _mapper;
        Mock<IUnitOfWork> mockUOF;
        IOrderService orderService;
        #region data
        static Order order = new Order() {
            Id = 1,
            OrderStatusId = 1,
            Email = "lalala@gmail.com",
            CartOrderItems = new List<CartOrderItem>()
            {
                new CartOrderItem()
                {
                    Quantity = 1,
                    CartId = "4596490f-524f-4af2-bf72-16f15bd78831",
                    Price = 4
                }
            }
        };
        OrderEntity orderentity;
        List<Order> orders = new List<Order>(){  
            new Order() {
            Id = 1,
            OrderStatusId = 1,
            Email = "lalala@gmail.com",
            CartOrderItems = expectedfullList.ToList()
        } };
        List<OrderStatus> statuses = new List<OrderStatus>(){ 
            new OrderStatus() { Id = 1, Name = "Payment received"},
            new OrderStatus() { Id = 2, Name = "Payment failed" }};

        static ICollection<CartOrderItem> expectedfullList = new List<CartOrderItem>()
        {
            new CartOrderItem()
            {
                Order = order,
                OrderId = order.Id,
                Quantity = 1,
                CartId = "4596490f-524f-4af2-bf72-16f15bd78831",
                Price = 4 }
            };
        ICollection<CartOrderItemEntity> entities;
        #endregion

        [SetUp]
        public void SetUp()
        {
            _mapper = UnitTestsHelper.CreateMapperProfile();
            orderentity = _mapper.Map<OrderEntity>(order);

            mockUOF = new Mock<IUnitOfWork>();

            orderService = new OrderService(mockUOF.Object, _mapper);
        }

        [Test]
        public void AddOrderAndAddress_OrderDTOPassed_EqualIntExpected()
        {
            //arrange
            
            mockUOF.Setup(x => x.OrderRepository.Add(_mapper.Map<OrderEntity>(order)));
            //act
            var actual = orderService.AddOrderAndAddress(order);

            //assert
            mockUOF.Verify(x => x.OrderRepository.Add(_mapper.Map<OrderEntity>(order)), Times.Never);
            Assert.AreEqual(order.Id, actual);
        }
        
        [Test]
        public void GetOrderById_OrderIdPassed_ItemTypeofOrderExpected()
        {
            //arrange
            var orderID = 1;
            mockUOF.Setup(x => x.OrderRepository.Get(orderID)).Returns(orderentity);

            //act
            var actual = orderService.GetOrderById(orderID);

            //assert
            Assert.IsInstanceOf<Order>(actual);
        }

        [Test]
        public void GetStockById_StockIdPassed_EqualStockDTOExpected()
        {
            //arrange
            var orderID = 1;
            mockUOF.Setup(x => x.OrderRepository.Get(orderID)).Returns(orderentity);

            //act
            var actual = orderService.GetOrderById(orderID);

            //assert
            Assert.AreEqual(order.Id, actual.Id);
        }
        [Test]
        public void GetOrderStatusByName_StringPassed_StockDTOExpected()
        {
            //arrange
            var name = statuses[1].Name;
            var statusentities = _mapper.Map<List<OrderStatusEntity>>(statuses);

            mockUOF.Setup(x => x.OrderStatusRepository.GetAll()).Returns(statusentities);

            //act
            var actual = orderService.GetOrderStatusByName(name);

            //assert
            Assert.AreEqual(statuses[1].Id, actual);
        }
        [Test]
        public void AddPaymentToOrder_IdAndStockIdPassed_OrderStatusPaymentRecieved()
        {
            //arrange
            var id = "1";
            var orderID = 1;
            var statusentities = _mapper.Map<List<OrderStatusEntity>>(statuses);
            mockUOF.Setup(x => x.OrderRepository.Get(orderID)).Returns(orderentity);
            mockUOF.Setup(x => x.OrderStatusRepository.GetAll()).Returns(statusentities);

            //act
            orderService.AddPaymentToOrder(id, orderID);

            //assert
            Assert.AreEqual(1, orderentity.OrderStatusId);
        }

        [Test]
        public void ChangeOrderStatus_NewStatusIdPassed_OrderStatusPaymentFailed()
        {
            //arrange
            var statusId = statuses[1].Id;
            var orderID = 1;
            mockUOF.Setup(x => x.OrderRepository.Get(orderID)).Returns(orderentity);

            //act
            orderService.ChangeOrderStatus(orderID, statusId);

            //assert
            Assert.AreEqual(statusId, orderentity.OrderStatusId);
            Assert.AreNotEqual(statuses[0].Id, orderentity.OrderStatusId);
        }
        [Test]
        public void GetAllOrdersFromEmail_EmailPassed_IEnumerableOrdersExpected()
        {
            //arrange
            var email = "lalala@gmail.com";
            mockUOF.Setup(x => x.OrderRepository.GetAll())
                .Returns(_mapper.Map<List<OrderEntity>>(orders));

            //act
            var actual = orderService.GetAllOrdersFromEmail(email).ToList();

            //assert
            Assert.True(actual != null);
            Assert.AreEqual(orders.Count, actual.Count);

            for (int i = 0; i < orders.Count; i++)
            {
                Assert.AreEqual(orders[0].Email, actual[0].Email);
            }
        }

        [Test]
        public void GetAllOrdersFromEmail_EmailPassed__IEnumerableExpected()
        {
            //arrange
            var email = "lalala@gmail.com";
            mockUOF.Setup(x => x.OrderRepository.GetAll()).Returns(_mapper.Map<List<OrderEntity>>(orders));
            //act
            var actual = orderService.GetAllOrdersFromEmail(email);

            //assert
            Assert.IsInstanceOf<IEnumerable<Order>>(actual);
        }
        
        [Test]
        public void GetAllOrderItems_OrderIdPassed_IEnumerableCartOrderItemsExpected()
        {
            //arrange
            var orderId = 1;
            entities = _mapper.Map<ICollection<CartOrderItemEntity>>(expectedfullList);
            mockUOF.Setup(x => x.OrderRepository.Get(orderId).CartOrderItems)
                .Returns(entities);

            //act
            var actual = orderService.GetAllOrderItems(orderId).ToList();
            //assert
            Assert.True(actual != null);
            Assert.AreEqual(order.CartOrderItems.Count, actual.Count);

            for (int i = 0; i < order.CartOrderItems.Count; i++)
            {
                Assert.AreEqual(order.CartOrderItems[0].Price, actual[0].Price);
            }
        }

        [Test]
        public void GetAllOrderItems_OrderIdPassed_IEnumerableExpected()
        {
            //arrange
            var orderId = 1;
            entities = _mapper.Map<ICollection<CartOrderItemEntity>>(expectedfullList);
            mockUOF.Setup(x => x.OrderRepository.Get(orderId).CartOrderItems)
                .Returns(entities);
            //act
            var actual = orderService.GetAllOrderItems(orderId);

            //assert
            Assert.IsInstanceOf<IEnumerable<CartOrderItem>>(actual);
        }
    }
}
