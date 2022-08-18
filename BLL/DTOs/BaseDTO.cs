using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
	public abstract class BaseDTO
	{
		private int _id;

		public int Id { get => _id; set => _id = value; }
	}
}
