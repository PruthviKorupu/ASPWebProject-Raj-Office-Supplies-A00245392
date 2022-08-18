using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
	public abstract class BaseEntity : IBaseEntity
	{
		private int _id;

		[Key]
		[Required]
		public int Id { get => _id; set => _id = value; }
	}
}
