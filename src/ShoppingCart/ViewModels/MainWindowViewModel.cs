using System.Collections.ObjectModel;
using System.Linq;
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

		public ObservableCollection<CartItemViewModel> Cart { get; }

		public DelegateCommand RefreshProducts { get; }

		public DelegateCommand<IProduct> AddToCart { get; }

		public DelegateCommand<CartItemViewModel> RemoveFromCart { get; }

		public DelegateCommand ClearCart { get; }

		private decimal _subtotal;
		public decimal Subtotal
		{
			get => _subtotal;
			set => SetProperty(ref _subtotal, value);
		}

		private decimal _tax = 0.0725m;
		public decimal Tax
		{
			get => _tax;
		}

		private decimal _shipping = 10m;
		public decimal Shipping
		{
			get => _shipping;
		}

		private decimal _total;
		public decimal Total
		{
			get => _total;
			set => SetProperty(ref _total, value);
		}

		public MainWindowViewModel(ProductsService productsService)
		{
			_productsService = productsService;

			Products = new ObservableCollection<IProduct>();
			Cart = new ObservableCollection<CartItemViewModel>();
			RefreshProducts = new DelegateCommand(FetchProducts);
			AddToCart = new DelegateCommand<IProduct>(AddProductToCart);
			RemoveFromCart = new DelegateCommand<CartItemViewModel>(RemoveProductFromCart);
			ClearCart = new DelegateCommand(RemoveAllFromCart);

			FetchProducts();
		}

		private async void FetchProducts()
		{
			Products.Clear();
			var products = await _productsService.GetAll();
			Products.AddRange(products);
		}

		private void AddProductToCart(IProduct product)
		{
			if (product == null)
				return;

			var cartItem = Cart.FirstOrDefault(x => x.Product.Id == product.Id);
			if (cartItem == null)
			{
				cartItem = new CartItemViewModel(product);
				Cart.Add(cartItem);
			}

			cartItem.Quantity++;
			UpdateTotal();
		}

		private void RemoveProductFromCart(CartItemViewModel product)
		{
			if (product == null)
				return;

			if (product.Quantity == 1)
				Cart.Remove(product);
			else
				--product.Quantity;

			UpdateTotal();
		}

		private void RemoveAllFromCart()
		{
			Cart.Clear();
			UpdateTotal();
		}

		private void UpdateTotal()
		{
			Subtotal = Cart.Sum(x => x.Total);
			Total = Subtotal > 0.00m ? Subtotal * (1 + Tax) + Shipping : 0.00m;
		}

		public class CartItemViewModel : BindableBase
		{
			public IProduct Product { get; }

			private int _quantity = 0;
			public int Quantity
			{
				get => _quantity;
				set
				{
					SetProperty(ref _quantity, value);
					UpdateTotal();
				}
			}

			private decimal _total;
			public decimal Total
			{
				get => _total;
				set => SetProperty(ref _total, value);
			}

			public CartItemViewModel(IProduct product)
			{
				Product = product;
			}

			private void UpdateTotal()
			{
				Total = Quantity * Product.Price;
			}
		}
	}
}
