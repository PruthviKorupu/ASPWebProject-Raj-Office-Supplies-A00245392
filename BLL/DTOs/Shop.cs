using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
	public class Shop : BaseDTO
	{
		private string _name;
		private string _description;
		private string _picturePath;
		private string _address;

		public string Name { get => _name; set => _name = value; }
		public string Description { get => _description; set => _description = value; }
		public string Address { get => _address; set => _address = value; }
		public string PicturePath { get => _picturePath; set => _picturePath = value; }
	}
}
