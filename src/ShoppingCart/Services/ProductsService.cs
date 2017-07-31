using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingCart.Api.Models;

namespace ShoppingCart.Services
{
	public class ProductsService
	{
		public async Task<ICollection<IProduct>> GetAll()
		{
			var client = new HttpClient
			{
				BaseAddress = new Uri("http://localhost:53265")
			};

			var response = await client.GetStringAsync("api/products");
			return JsonConvert.DeserializeObject<ICollection<IProduct>>(response);
		}
	}
}
