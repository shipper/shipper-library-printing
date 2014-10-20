using System;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public abstract class DrawingElement
	{
		public int X { get; set; }
		public int Y { get; set; }

		public abstract void Draw(IDrawingClient client);

		public void Draw(Graphics graphics){
			using (DrawingClient client = new DrawingClient()) {
				Draw (client);
				client.Draw (graphics);
			}
		}
	}
}

