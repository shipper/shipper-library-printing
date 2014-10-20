using System;
using System.Collections.Generic;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Elements;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing
{
	public class DrawingDocument 
	{
		public int Width { get; set; }
		public int Height { get; set; }
		
		public IEnumerable<DrawingString> Strings { get; set; }
		public IEnumerable<DrawingImage> Images { get; set; }
		public IEnumerable<DrawingRectangle> Rectangles { get; set; }
		public IEnumerable<DrawingLine> Lines { get; set; }

		public void Draw(IDrawingClient client){
			Draw (client, Strings);
			Draw (client, Images);
			Draw (client, Rectangles);
			Draw (client, Lines);
		}

		public void Draw(Graphics graphics){
			using (DrawingClient client = new DrawingClient()) {
				Draw (client);
				client.Draw (graphics);
			}
		}

		private void Draw(IDrawingClient client, IEnumerable<DrawingElement> elements){
			foreach (DrawingElement element in elements) {
				element.Draw (client);
			}
		}
	}
}

