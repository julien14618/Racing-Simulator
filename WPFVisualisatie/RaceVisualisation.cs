using System;
using Controller;
using Model;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace WPFVisualisatie
{
    public static class RaceVisualisation
    {
        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap bm = CreateImages.CreateEmptyBitmap(50, 50);
            return CreateImages.CreateBitmapSourceFromGdiBitmap(bm);
        }
    }
}
