using BLL.DTOs;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RajOfficeSupplies.Models;

namespace IntegrationTests.Tests
{
	public class ShopIntegrationTests
	{
        private HttpClient _client;
        private WebAppFactory _factory;
        private const string RequestUri = "api/Shop/";

        [SetUp]
        public void Setup()
        {
            _factory = new WebAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task ShopController_AddShop_ShopsIncreased()
        {
            // Arrange
            Shop shop = new Shop() { Name = "Test" };
            var json = JsonConvert.SerializeObject(shop);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, data);

            // Act
            httpResponse.EnsureSuccessStatusCode();

            var assertResponse = await _client.GetAsync("api/Home/");
            var stringResponse = await assertResponse.Content.ReadAsStringAsync();
            var shopsAndCategories = JsonConvert.DeserializeObject<HomePageModel>(stringResponse);

            //Assert
            shopsAndCategories.Shops.Count.Should().Be(5);
        }

        [Test]
        public async Task ShopController_GetShopById_ReturnsShop()
        {
            // Arrange
            var httpResponse = await _client.GetAsync(RequestUri + 2);

            // Act
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var shop = JsonConvert.DeserializeObject<Shop>(stringResponse);

            //Assert
            shop.Name.Should().Be("RajOfficeSupplies");
        }

        [Test]
        public async Task ShopController_DeleteShopById_OneLessShop()
        {
            // Arrange
            var deleteResponse = await _client.DeleteAsync(RequestUri + 2);

            // Act
            deleteResponse.EnsureSuccessStatusCode();
            var assertResponse = await _client.GetAsync("api/Home/");
            var stringResponse = await assertResponse.Content.ReadAsStringAsync();
            var shopsAndCategories = JsonConvert.DeserializeObject<HomePageModel>(stringResponse);

            //Assert
            shopsAndCategories.Shops.Count.Should().Be(3);
        }
    }
}
