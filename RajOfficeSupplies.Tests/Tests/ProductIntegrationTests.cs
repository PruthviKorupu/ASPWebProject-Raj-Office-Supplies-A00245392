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
	[TestFixture]
	public class ProductIntegrationTests
	{
        private HttpClient _client;
        private WebAppFactory _factory;
        private const string RequestUri = "api/Product/";

        [SetUp]
        public void Setup()
        {
            _factory = new WebAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task ProductController_GetProductById_ReturnsProduct()
        {
            // Arrange
            var httpResponse = await _client.GetAsync(RequestUri + 2);

            // Act
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<StockModel>(stringResponse);

            //Assert
            product.Stock.Quantity.Should().Be(30);
            product.Stock.Price.Should().Be(21);
        }

        [Test]
        public async Task ProductController_GetAllProducts_ReturnsProducts()
        {
            // Arrange
            var httpResponse = await _client.GetAsync(RequestUri);

            // Act
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<StocksModel>(stringResponse);

            //Assert
            products.Stocks.Count.Should().Be(28);
        }
    }
}
