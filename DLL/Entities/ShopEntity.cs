using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	[Table("Shops")]
	public class ShopEntity : BaseEntity
	{
		private string _name;
		private string _description;
		private string _picturePath;
		private string _address;
		
		[Required]
		public string Name { get => _name; set => _name = value; }
		public string Description { get => _description; set => _description = value; }
		public string Address { get => _address; set => _address = value; }
		public string PicturePath { get => _picturePath; set => _picturePath = value; }
	}
}
