using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace DAL.Entities
{
	[Table("ProductCategories")]
	public class ProductCategoryEntity : BaseEntity
	{
		private string _name;
		
		[Required]
		public string Name { get => _name; set => _name = value; }
	}
}
