using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingCartModels;

namespace ShoppingCart.Services
{
	public class ProductsService : IDisposable
	{
		private readonly JsonSerializerSettings _settings;
		private readonly HttpClient _client;

		public ProductsService()
		{
			_client = new HttpClient
			{
				BaseAddress = new Uri("http://localhost:53266")
			};
			_settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

		}

		public async Task<ICollection<IProduct>> GetAll()
		{
			var response = await _client.GetStringAsync("api/products");
			return JsonConvert.DeserializeObject<ICollection<IProduct>>(response, _settings);
		}

		public void Dispose()
		{
			_client?.Dispose();
		}
	}
}
