using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	[Table("Products")]
	public class ProductEntity : BaseEntity
	{
		private string _name;
		private string _description;
		private string _picturePath;
		private ProductCategoryEntity _category;
		
		[Required]
		public string Name { get => _name; set => _name = value; }
		public string Description { get => _description; set => _description = value; }
		[Required]
		public string PicturePath { get => _picturePath; set => _picturePath = value; }
		[Required]
		public virtual ProductCategoryEntity Category { get => _category; set => _category = value; }
	}
}
