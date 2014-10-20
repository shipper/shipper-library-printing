using System;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public class DrawingString : DrawingBrushElement
	{
		public string Content { get; set; }

		public string FontFamily { get; set;}

		public float EmphasisSize { get; set; }

		public System.Drawing.Font Font {
			get {
				if (String.IsNullOrWhiteSpace (FontFamily) && EmphasisSize <= 0) {
					return new System.Drawing.Font (System.Drawing.FontFamily.GenericSansSerif, 8);
				}
				if (String.IsNullOrWhiteSpace (FontFamily)) {
					return new System.Drawing.Font (System.Drawing.FontFamily.GenericSansSerif, EmphasisSize);
				}
				if (EmphasisSize <= 0) {
					EmphasisSize = 0;
				}
				return new System.Drawing.Font (FontFamily, EmphasisSize);
			}
		}

		public override void Draw(IDrawingClient client){
			client.DrawString (Content, Font, Brush, X, Y);
		}
	}
}

