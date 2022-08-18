using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	[Table("Addresses")]
	public class AddressEntity : BaseEntity
	{
		private string _fullName;
		private string _street;
		private string _city;
		private string _zip;

		[Required]
		public string FullName { get => _fullName; set => _fullName = value; }
		[Required]
		public string Street { get => _street; set => _street = value; }
		[Required]
		public string City { get => _city; set => _city = value; }
		public string Zip { get => _zip; set => _zip = value; }

		public AddressEntity()
		{

		}

		public AddressEntity(string fullName)
		{
			_fullName = fullName;
		}
	}
}
