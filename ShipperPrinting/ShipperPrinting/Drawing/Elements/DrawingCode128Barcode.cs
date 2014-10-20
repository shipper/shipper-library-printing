using System;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public class DrawingCode128Barcode : DrawingImage
	{
		public int BarWeight { get; set; }
		public bool AddQuiteZone { get; set; }

		public override void Draw (IDrawingClient client)
		{
			if (BarWeight < 1) {
				BarWeight = 1;
			}
			client.DrawCode128Barcode (Content, 
			                           X, 
			                           Y,
			                           BarWeight, 
			                           AddQuiteZone, 
			                           RotateFlipType ?? System.Drawing.RotateFlipType.RotateNoneFlipNone);
		}
	}
}

