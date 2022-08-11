using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RajOfficeSuppliesAPI.Controllers
{
	[Route("api/Category")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpPost]
		public ActionResult<ProductCategory> AddCategory(ProductCategory category)
		{
			_categoryService.AddCategory(category);
			return CreatedAtAction(nameof(AddCategory), new { id = category.Id }, category);
		}

		[HttpGet("{id}")]
		public ActionResult<ProductCategory> GetCategory(int id)
		{
			return Ok(_categoryService.GetCategory(id));
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteCategory(int id)
		{
			ProductCategory category = _categoryService.GetCategory(id);
			if (category == null)
			{
				return BadRequest();
			}

			_categoryService.DeleteCategory(id);
			return NoContent();
		}
	}
}
