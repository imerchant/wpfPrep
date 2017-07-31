using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.Services;

namespace ShoppingCart.Api.Controllers
{
	[Route("api/[controller]")]
	public class ProductsController : Controller
	{
		private readonly IProductsService _productsService;

		public ProductsController(IProductsService productsService)
		{
			_productsService = productsService;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(_productsService.GetAll());
		}
	}
}
