using System;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public class DrawingString : DrawingBrushElement, IDynamicElement
	{
		public string Id { get; set; }

		public string Content { get; set; }

		public string FontFamily { get; set;}

		public float EmphasisSize { get; set; }

		public FontStyle? FontStyle { get; set; }

		public System.Drawing.Font Font {
			get {
				FontStyle style = FontStyle ?? System.Drawing.FontStyle.Regular;
				if (String.IsNullOrWhiteSpace (FontFamily) && EmphasisSize <= 0) {
					return new System.Drawing.Font (System.Drawing.FontFamily.GenericSansSerif, 8, style);
				}
				if (String.IsNullOrWhiteSpace (FontFamily)) {
					return new System.Drawing.Font (System.Drawing.FontFamily.GenericSansSerif, EmphasisSize, style);
				}
				if (EmphasisSize <= 0) {
					EmphasisSize = 0;
				}
				return new System.Drawing.Font (FontFamily, EmphasisSize, style);
			}
		}

		public override void Draw(IDrawingClient client){
			client.DrawString (Content, Font, Brush, X, Y);
		}
	}
}

