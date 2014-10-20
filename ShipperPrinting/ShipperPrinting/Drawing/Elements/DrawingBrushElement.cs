using System;
using System.Drawing;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public abstract class DrawingBrushElement : DrawingElement
	{
		public int BrushRed { get; set; }
		public int BrushGreen { get; set; }
		public int BrushBlue { get; set; }

		public int PenRed { get; set; }
		public int PenGreen { get; set; }
		public int PenBlue { get; set; }

		public float PenWidth { get; set; }

		public Brush Brush {
			get
			{
				return GetBrush (BrushRed, BrushGreen, BrushBlue);
			}
		}

		public Pen Pen {
			get 
			{
				return GetPen (PenRed, PenGreen, PenBlue, PenWidth);
			}
		}
		
		private static Pen GetPen(int red, int green, int blue, float width = 1f) {
			Color color = GetColour (red, green, blue);
			width = width > 0f ? width : 1f;
			return new Pen (color, width);
		}

		private static Brush GetBrush(int red, int green, int blue) {
			Color color = GetColour (red, green, blue);
			return new SolidBrush (color);
		}

		private static Color GetColour(int red, int green, int blue) { 
			return Color.FromArgb (red, green, blue);
		}
	}
}

