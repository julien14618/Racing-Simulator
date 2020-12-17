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
        static private Dictionary<string, Bitmap> imageCache;

        public static void Initialise()
        {
            imageCache = new Dictionary<string, Bitmap>();
        }
        static public Bitmap GetImageFromCache(string url)
        {
            if (imageCache.TryGetValue(url, out Bitmap result))
                return result;
            result = new Bitmap(url);
            imageCache.Add(url, result);
            return result;
        }

        static public void ClearCache()
        {
            imageCache?.Clear();
        }
        static public Bitmap CreateEmptyBitmap(int width, int height)
        {
            if (imageCache.ContainsKey("empty"))
                return (Bitmap)GetImageFromCache("empty").Clone();
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            SolidBrush color = new SolidBrush(System.Drawing.Color.Yellow);
            g.FillRectangle(color, 0, 0, width, height);
            imageCache.Add("empty", bm);
            return (Bitmap)bm.Clone();
            
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
