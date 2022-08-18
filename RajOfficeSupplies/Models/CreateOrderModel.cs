using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RajOfficeSupplies.Models
{
	public class CreateOrderModel
	{
		[Required]
		[Display(Name = "Full name")]
		public string FullName { get; set; }
		[Required]
		public string Street { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string Zip { get; set; }
	}
}
