using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Extra.vMixSnapshots
{
    static class ImageUtils
    {
        public static async Task<BitmapImage> TryGetBitmapAsync(string file)
        {
            return await CreateBitmapFromFileAsync(file);
        }

        private static async Task<BitmapImage> CreateBitmapFromFileAsync(string absoluteFilePath, int decodePixelWidth = 0, bool cacheOnLoad = true)
        {
            BitmapImage image = null;
            await Task.Factory.StartNew(() =>
            {
                image = CreateImage(absoluteFilePath, decodePixelWidth, cacheOnLoad);
            },
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskScheduler.FromCurrentSynchronizationContext());

            return image;
        }

        private static BitmapImage CreateImage(string absoluteFilePath, int decodePixelWidth, bool cacheOnLoad)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();

            if (decodePixelWidth > 0)
                bitmap.DecodePixelWidth = decodePixelWidth;

            bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;

            if (cacheOnLoad)
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
            else
                bitmap.CacheOption = BitmapCacheOption.None;

            bitmap.UriSource = new Uri(absoluteFilePath, UriKind.Absolute);
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
    }
}
