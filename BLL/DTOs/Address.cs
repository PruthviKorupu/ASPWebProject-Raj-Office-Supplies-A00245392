using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
	public class Address : BaseDTO
	{
		private string _fullName;
		private string _street;
		private string _city;
		private string _zip;

		public string FullName { get => _fullName; set => _fullName = value; }
		public string Street { get => _street; set => _street = value; }
		public string City { get => _city; set => _city = value; }
		public string Zip { get => _zip; set => _zip = value; }
	}
}
