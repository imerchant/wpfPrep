namespace ShoppingCart.Api.Models
{
	public interface IProduct
	{
		int Id { get; }
		string Name { get; }
		string Description { get; }
		string ImageUrl { get; }
		decimal Price { get; }
		string By { get; }
	}

	public class Book : IProduct
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; } = "http://via.placeholder.com/50x50";
		public decimal Price { get; set; }
		public string Author { get; set; }
		
		public string By => Author;
	}

	public class Computer : IProduct
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; } = "http://via.placeholder.com/50x50";
		public decimal Price { get; set; }
		public string Manufacturer { get; set; }

		public string By => Manufacturer;
	}

	public class Album : IProduct
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; } = "http://via.placeholder.com/50x50";
		public decimal Price { get; set; }
		public string Artist { get; set; }
		public string Format { get; set; }

		public string By => Artist;
	}
}
