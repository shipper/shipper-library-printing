using System;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public class DrawingLine : DrawingBrushElement
	{
		public int Width { get; set; }
		public int Height { get; set; }
		
		public override void Draw(IDrawingClient client){
			client.DrawLine (Pen, X, Y, X + Width, Y + Height);
		}
	}
}

