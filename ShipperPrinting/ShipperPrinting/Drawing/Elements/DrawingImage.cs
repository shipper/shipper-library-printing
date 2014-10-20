using System;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public class DrawingImage : DrawingElement
	{
		public string Content { get; set; }
		public RotateFlipType? RotateFlipType { get; set; }

		public override void Draw(IDrawingClient client){
			client.DrawImage (Content, 
			                  X, 
			                  Y, 
			                  RotateFlipType ?? System.Drawing.RotateFlipType.RotateNoneFlipNone);
		}
	}
}

