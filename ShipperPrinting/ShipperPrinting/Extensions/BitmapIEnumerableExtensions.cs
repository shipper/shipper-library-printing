using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;

internal static class BitmapIEnumerableExtensions
{
	public static Bitmap Join(this IEnumerable<Bitmap> bitmaps){
		if (bitmaps == null) {
			return new Bitmap (0, 0);
		}
		int width = bitmaps.Max(b => b.Width);
		int height = bitmaps.Sum(b => b.Height);
		Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
		int y = 0;
		using(Graphics graphics = Graphics.FromImage(result)){
			foreach (Bitmap bitmap in bitmaps) {
				graphics.DrawImage (bitmap, 0, y, bitmap.Width, bitmap.Height);
				y += bitmap.Height;
			}
		}
        return result;
	}
}

