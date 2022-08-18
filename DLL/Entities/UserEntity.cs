using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
	public class UserEntity : IdentityUser
	{
		private string _fullName;
		
		[Required]
		public string FullName { get => _fullName; set => _fullName = value; }
	}
}
