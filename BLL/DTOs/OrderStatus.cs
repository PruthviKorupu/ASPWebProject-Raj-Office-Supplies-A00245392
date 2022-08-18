using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
	public class OrderStatus : BaseDTO
	{
		private string _name;

		public string Name { get => _name; set => _name = value; }
	}
}
