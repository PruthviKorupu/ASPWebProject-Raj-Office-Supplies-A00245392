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
	public class CategoryIntegrationTests
	{
        private HttpClient _client;
        private WebAppFactory _factory;
        private const string RequestUri = "api/Category/";

        [SetUp]
        public void Setup()
        {
            _factory = new WebAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CategoryController_AddCategory_CategoriesIncreased()
        {
            // Arrange
            ProductCategory category = new ProductCategory() { Name = "Test" };
            var json = JsonConvert.SerializeObject(category);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, data);

            // Act
            httpResponse.EnsureSuccessStatusCode();
            
            var assertResponse = await _client.GetAsync("api/Home/");
            var stringResponse = await assertResponse.Content.ReadAsStringAsync();
            var shopsAndCategories = JsonConvert.DeserializeObject<HomePageModel>(stringResponse);

            //Assert
            shopsAndCategories.Categories.Count.Should().Be(9);
        }

        [Test]
        public async Task CategoryController_GetCategoryById_ReturnsCategory()
        {
            // Arrange
            var httpResponse = await _client.GetAsync(RequestUri + 2);

            // Act
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<ProductCategory>(stringResponse);

            //Assert
            category.Name.Should().Be("Books");
        }

        [Test]
        public async Task CategoryController_DeleteCategoryById_OneLessCategory()
        {
            // Arrange
            var deleteResponse = await _client.DeleteAsync(RequestUri + 2);

            // Act
            deleteResponse.EnsureSuccessStatusCode();
            var assertResponse = await _client.GetAsync("api/Home/");
            var stringResponse = await assertResponse.Content.ReadAsStringAsync();
            var shopsAndCategories = JsonConvert.DeserializeObject<HomePageModel>(stringResponse);

            //Assert
            shopsAndCategories.Categories.Count.Should().Be(7);
        }
    }
}
