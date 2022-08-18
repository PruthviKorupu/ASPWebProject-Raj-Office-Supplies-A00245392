using AutoMapper;
using BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace RajOfficeSuppliesTests.BLLTests
{
    internal static class UnitTestsHelper
    {
		public static Mapper CreateMapperProfile()
		{
			var myProfile = new CustomMapper();
			var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

			return new Mapper(configuration);
		}
	}
}
