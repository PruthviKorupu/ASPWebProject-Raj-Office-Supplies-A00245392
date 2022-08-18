using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
	public class StockService : IStockService
	{

		private readonly IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public StockService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<Stock> GetAllStocks()
		{
			IEnumerable<StockEntity> stockEntities = _unitOfWork.StockRepository.GetAll();
			return _mapper.Map<IEnumerable<Stock>>(stockEntities);
		}

		public Stock GetStockById(int id)
		{
			StockEntity stockEntity = _unitOfWork.StockRepository.Get(id);
			Stock stock = _mapper.Map<Stock>(stockEntity);
			return stock;
		}
	}
}
