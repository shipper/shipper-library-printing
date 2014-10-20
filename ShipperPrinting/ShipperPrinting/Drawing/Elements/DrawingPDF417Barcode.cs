using System;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public class DrawingPDF417Barcode: DrawingImage
	{
		public override void Draw (IDrawingClient client)
		{
			client.DrawPDF417Barcode (Content, 
			                           X, 
			                           Y,
			                           RotateFlipType ?? System.Drawing.RotateFlipType.RotateNoneFlipNone);
		}
	}
}

