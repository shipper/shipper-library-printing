using System;
using System.Drawing;
using System.IO;

namespace Charles.Shipper.Printing.Core.Drawing.Interfaces
{
	public interface IDrawingClient
	{
		int Width {get;set;}
		int Height {get;set;}
		Font DefaultFont { get; set; }
		Brush DefaultBrush { get; set; }
		Pen DefaultPen { get; set; }

		void TranslateTransform (float dx, float dy);
		void RotateTransform (float angle);
		
		void DrawRectangle (int x, int y, int width, int height);
		void DrawRectangle (float x, float y, float width, float height);
		void DrawRectangle (Pen pen, int x, int y, int width, int height);
		void DrawRectangle (Pen pen, float x, float y, float width, float height);

		void DrawLine (int x1, int y1, int x2, int y2);
		void DrawLine (float x1, float y1, float x2, float y2);
		void DrawLine (Pen pen, int x1, int y1, int x2, int y2);
		void DrawLine (Pen pen, float x1, float y1, float x2, float y2);
		
		void DrawString (String value, PointF point);
		void DrawString (String value, RectangleF point);
		void DrawString (String value, PointF point, StringFormat format);
		void DrawString (String value, RectangleF point, StringFormat format);
		void DrawString (String value, float x, float y);
		void DrawString (String value, float x, float y, StringFormat format);

		void DrawString (String value, Font font, Brush brush, PointF point);
		void DrawString (String value, Font font, Brush brush, RectangleF point);
		void DrawString (String value, Font font, Brush brush, PointF point, StringFormat format);
		void DrawString (String value, Font font, Brush brush, RectangleF point, StringFormat format);
		void DrawString (String value, Font font, Brush brush, float x, float y);
		void DrawString (String value, Font font, Brush brush, float x, float y, StringFormat format);

		void DrawCode128Barcode(String value, int x, int y, RotateFlipType rotateFlipType);
		void DrawCode128Barcode(String value, int x, int y, int barWeight, bool addQuietZone, RotateFlipType rotateFlipType);

		void DrawPDF417Barcode (String value, int x, int y, RotateFlipType rotateFlipType);
		void DrawPDF417Barcode (String value, int x, int y, int scaleX, int scaleY, RotateFlipType rotateFlipType);

		void DrawImageUnscaled(Image image, int x, int y);
		void DrawImageUnscaled(Image image, int x, int y, RotateFlipType rotateFlipType);

		void DrawImage (string base64, int x, int y);
		void DrawImage (string base64, int x, int y, RotateFlipType rotateFlipType);

		void DrawImage (MemoryStream image, int x, int y);
		void DrawImage (MemoryStream image, int x, int y, RotateFlipType rotateFlipType, bool dispose);

		void DrawImage (Image image, int x, int y);
		void DrawImage (Image image, int x, int y, RotateFlipType rotateFlipType);

		void AddCommand(IDrawingCommand command);
		void Draw(Graphics graphics);
	}
}

