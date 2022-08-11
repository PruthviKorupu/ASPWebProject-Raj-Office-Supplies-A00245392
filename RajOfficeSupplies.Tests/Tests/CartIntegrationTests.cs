using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RajOfficeSupplies.Models;
using FluentAssertions;
using BLL.DTOs;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class CartIntegrationTests
    {
        private HttpClient _client;
        private WebAppFactory _factory;
        private const string RequestUri = "api/Cart/";

        [SetUp]
        public void Setup()
        {
            _factory = new WebAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CartController_GetCartById_ReturnsCart()
        {
            // Arrange
            var httpResponse = await _client.GetAsync(RequestUri + "testId");

            // Act
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<CartModel>(stringResponse);

			//Assert
			cart.Items.Count.Should().Be(2);
            foreach (CartOrderItem c in cart.Items)
			{
                c.CartId = "testId";
            }
        }

		[Test]
		public async Task CartController_DeleteItemById_OneLessItemInCart()
		{
            // Arrange
            var deleteResponse = await _client.DeleteAsync(RequestUri + 2);

            // Act
            deleteResponse.EnsureSuccessStatusCode();
            var httpResponse = await _client.GetAsync(RequestUri + "testId");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<CartModel>(stringResponse);

            // Assert
            cart.Items.Count.Should().Be(3);
        }

    }
}
