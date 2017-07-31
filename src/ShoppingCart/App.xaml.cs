using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using ShoppingCart.Services;

namespace ShoppingCart
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			IUnityContainer container = new UnityContainer();
			container.RegisterInstance(new ProductsService());
			ViewModelLocationProvider.SetDefaultViewModelFactory(type => container.Resolve(type));
		}
	}
}
