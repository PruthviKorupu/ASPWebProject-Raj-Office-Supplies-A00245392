﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RajOfficeSuppliesLibrary.Entities
{
	public class OrderStatusEntity : BaseEntity
	{
		private string _name;

		public string Name { get => _name; set => _name = value; }
	}
}
