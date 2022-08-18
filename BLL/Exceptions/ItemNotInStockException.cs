using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exceptions
{
	public class ItemNotInStockException : Exception
	{
		public ItemNotInStockException(string message) : base(message)
		{

		}
	}
}
