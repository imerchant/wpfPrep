using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using ShoppingCart.Api.Models;
using ShoppingCart.Services;

namespace ShoppingCart.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private readonly ProductsService _productsService;

		public ObservableCollection<IProduct> Products { get; }

		public ObservableCollection<IProduct> Cart { get; }

		public DelegateCommand RefreshProducts { get; }

		public DelegateCommand AddToCart { get; }

		public DelegateCommand RemoveFromCart { get; }

		public MainWindowViewModel(ProductsService productsService)
		{
			_productsService = productsService;

			Products = new ObservableCollection<IProduct>();
			Cart = new ObservableCollection<IProduct>();
			RefreshProducts = new DelegateCommand(FetchProducts);
		}

		private async void FetchProducts()
		{
			Products.Clear();
			var products = await _productsService.GetAll();
			Products.AddRange(products);
		}
	}
}
