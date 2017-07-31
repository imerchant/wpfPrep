using Prism.Mvvm;

namespace ShoppingCart.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private string _message = "Hello world";

		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}
	}
}
