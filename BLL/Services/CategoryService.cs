using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<ProductCategory> GetAllCategories()
		{
			IEnumerable<ProductCategoryEntity> categEntities = _unitOfWork.ProductCategoryRepository.GetAll();
			return _mapper.Map<IEnumerable<ProductCategory>>(categEntities);
		}

		public void AddCategory(ProductCategory category)
		{
			_unitOfWork.ProductCategoryRepository.Add(
				_mapper.Map<ProductCategoryEntity>(category));
			_unitOfWork.Save();
		}

		public ProductCategory GetCategory(int id)
		{
			return _mapper.Map<ProductCategory>(_unitOfWork.ProductCategoryRepository.Get(id));
		}

		public void DeleteCategory(int id)
		{
			_unitOfWork.ProductCategoryRepository.Delete(id);
			_unitOfWork.Save();
		}
	}
}
