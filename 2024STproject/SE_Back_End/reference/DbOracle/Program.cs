using Microsoft.EntityFrameworkCore;
using DbOracle.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
//using DbOracle.Filters;

namespace DbOracle
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var app = CreateWebHostBuilder(args).Build();

			// Configure the HTTP request pipeline.

			app.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
		//.UseUrls("http://*:6600");
	}
}