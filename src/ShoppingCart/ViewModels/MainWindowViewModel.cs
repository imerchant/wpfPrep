using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Nito.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using ShoppingCart.Services;
using ShoppingCartModels;

namespace ShoppingCart.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private readonly ProductsService _productsService;
		private readonly ImageService _imageService;

		public ObservableCollection<ProductViewModel> Products { get; }

		public ObservableCollection<CartItemViewModel> Cart { get; }

		public DelegateCommand RefreshProducts { get; }

		public DelegateCommand<ProductViewModel> AddToCart { get; }

		public DelegateCommand<CartItemViewModel> RemoveFromCart { get; }

		public DelegateCommand ClearCart { get; }

		private decimal _subtotal;
		public decimal Subtotal
		{
			get => _subtotal;
			set => SetProperty(ref _subtotal, value);
		}

		public decimal Tax { get; } = 0.0725m;

		public decimal Shipping { get; } = 10m;

		private decimal _total;
		public decimal Total
		{
			get => _total;
			set => SetProperty(ref _total, value);
		}

		public MainWindowViewModel(ProductsService productsService, ImageService imageService)
		{
			_productsService = productsService;
			_imageService = imageService;

			Products = new ObservableCollection<ProductViewModel>();
			Cart = new ObservableCollection<CartItemViewModel>();
			RefreshProducts = new DelegateCommand(FetchProducts);
			AddToCart = new DelegateCommand<ProductViewModel>(AddProductToCart);
			RemoveFromCart = new DelegateCommand<CartItemViewModel>(RemoveProductFromCart);
			ClearCart = new DelegateCommand(RemoveAllFromCart);

			FetchProducts();
		}

		private async void FetchProducts()
		{
			Products.Clear();
			var products = await _productsService.GetAll();
			Products.AddRange(products.Select(product => new ProductViewModel(product, p => _imageService.Get(p.ImageUrl))));
		}

		private void AddProductToCart(ProductViewModel product)
		{
			if (product == null)
				return;

			var cartItem = Cart.FirstOrDefault(x => x.Product.Id == product.Product.Id);
			if (cartItem == null)
			{
				cartItem = new CartItemViewModel(product.Product);
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
	}

	public class ProductViewModel : BindableBase
	{
		public NotifyTask<BitmapImage> ImageSource { get; }

		public IProduct Product { get; }

		public ProductViewModel(IProduct product, Func<IProduct, Task<BitmapImage>> imgFetcher)
		{
			Product = product;
			ImageSource = NotifyTask.Create(imgFetcher(Product));
		}
	}

	public class CartItemViewModel : BindableBase
	{
		public IProduct Product { get; }

		private int _quantity;
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
