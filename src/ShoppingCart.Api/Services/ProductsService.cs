using System.Collections.Generic;
using ShoppingCart.Api.Models;

namespace ShoppingCart.Api.Services
{
	public interface IProductsService
	{
		ICollection<IProduct> GetAll();
	}

	public class ProductsService : IProductsService
	{
		private readonly ICollection<IProduct> _products;

		public ProductsService()
		{
			_products = new List<IProduct>
			{
				new Book
				{
					Id = 1,
					Name = "The Fault In Our Stars",
					Author = "John Green",
					Price = 12m
				},
				new Book
				{
					Id = 2,
					Name = "American Gods",
					Author = "Neil Gaiman",
					Price = 15.50m
				},
				new Book
				{
					Id = 3,
					Name = "Leviathan Wakes",
					Author = "James S.A. Corey",
					Price = 7.39m
				},
				new Computer
				{
					Id = 4,
					Name = "Alienware x3",
					Manufacturer = "Dell",
					Price = 1500m
				},
				new Computer
				{
					Id = 5,
					Name = "Surface Pro",
					Manufacturer = "Microsoft",
					Price = 2199m
				},
				new Computer
				{
					Id = 6,
					Name = "HP Elite x3",
					Manufacturer = "HP",
					Price = 900m
				},
				new Album
				{
					Id = 7,
					Name = "Every Open Eye",
					Artist = "Chvrches",
					Format = "mp3",
					Price = 11.99m
				},
				new Album
				{
					Id = 8,
					Name = "Purple Rain",
					Artist = "Prince",
					Format = "CD",
					Price = 18.99m
				},
				new Album
				{
					Id = 9,
					Name = "One More Light",
					Artist = "Linkin Park",
					Format = "mp3",
					Price = 9.99m
				}
			};
		}

		public ICollection<IProduct> GetAll()
		{
			return _products;
		}
	}
}
