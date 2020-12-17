using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace WPFVisualisatie
{
    public static class CreateImages
    {
        static private Dictionary<string, Bitmap> cache;
        static public Bitmap GetImageFromCache(string url)
        {
            Bitmap result;
            if (!cache.TryGetValue(url, out result))
                cache.Add(url, new Bitmap(url));
            cache.TryGetValue(url, out result);
            return result;
        }

        static public void ClearCache()
        {
            cache.Clear();
        }
        static public Bitmap GetBitmap(int width, int height)
        {
            Bitmap bm = new Bitmap(GetImageFromCache("Empty"), width, height);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(System.Drawing.Color.Green);
            cache["Empty"] = bm;
            return (Bitmap)cache["Empty"].Clone();
            
        }
        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
