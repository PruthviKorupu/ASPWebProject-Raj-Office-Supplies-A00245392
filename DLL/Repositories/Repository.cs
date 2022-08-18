using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly DbSet<TEntity> _contextDbSet;
		private readonly RajOfficeSuppliesDbContext _context;

		public Repository(RajOfficeSuppliesDbContext context)
		{
			_contextDbSet = context.Set<TEntity>();
			_context = context;
		}

		public IEnumerable<TEntity> GetAll()
		{
			return _contextDbSet.ToList();
		}

		public void Add(TEntity entity)
		{
			_contextDbSet.Add(entity);
		}

		public TEntity Get(int id)
		{
			return _contextDbSet.Find(id);
		}

		public void Update(TEntity entity)
		{
			_contextDbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			TEntity tEntity = _contextDbSet.Find(id);
			if (tEntity != null)
			{
				_contextDbSet.Remove(tEntity);
			}
		}
	}
}
