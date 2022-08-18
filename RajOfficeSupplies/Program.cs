using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RajOfficeSupplies
{
	 
	public delegate void FirstDelegate();

	 
	public delegate string SecondDelegate();

 
	public delegate void ThirdDelegate(string msg);

	 
	public delegate void EventDelegate(string msg);

	public class Test
	{
	 
		public event EventDelegate Notify;
		
		public void TestMethod()
		{
		 
			Notify?.Invoke("This is message for the event from test method");
		}
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			 
			FirstDelegate firstDelegate = Msg1;
			SecondDelegate secondDelegate = Msg2;
			ThirdDelegate thirdDelegate = Msg3;
			 
			FirstDelegate firstDelegateWithAnonymousMethod = delegate () {
				System.Diagnostics.Debug.WriteLine("This is message for delegate 1 with anonymous method");
			};
		 
			ThirdDelegate thirdDelegateWithLambdaOperatorAndExpression = (x) => 
				System.Diagnostics.Debug.WriteLine("This is message for delegate 3 with parameter: " + x);


		 
			firstDelegate();
			System.Diagnostics.Debug.WriteLine("This is message for delegate 2 with return value: " + secondDelegate());
			thirdDelegate("Test parameter");
			firstDelegateWithAnonymousMethod();

			string msg = "Parameter for Lambda expression";
			thirdDelegateWithLambdaOperatorAndExpression(msg);

			 
			Test test = new Test();
		 
			test.Notify += EventMsg;
		 
			test.TestMethod();

			CreateHostBuilder(args).Build().Run();
		}
		private static void EventMsg(string msg)
		{
			System.Diagnostics.Debug.WriteLine(msg);
		}

		private static void Msg1()
		{
			System.Diagnostics.Debug.WriteLine("This is message for delegate 1");
		}

		private static string Msg2()
		{
			return "Return value for delegate 2";
		}

		private static void Msg3(string msg)
		{
			System.Diagnostics.Debug.WriteLine("This is message for delegate 3 with parameter: " + msg);
		}



		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
