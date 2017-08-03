using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ShoppingCart.Services
{
	public class ImageService
	{
		private readonly ConcurrentDictionary<string, BitmapImage> _images = new ConcurrentDictionary<string, BitmapImage>();
		private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

		public async Task<BitmapImage> Get(string url)
		{
			if (_images.TryGetValue(url, out BitmapImage hit))
				return hit;

			await _semaphore.WaitAsync();
			try
			{
				var image = _images.GetOrAdd(url, str => new BitmapImage(new Uri(str, UriKind.Absolute)));
				return image;
			}
			finally
			{
				_semaphore.Release();
			}
		}
	}
}
