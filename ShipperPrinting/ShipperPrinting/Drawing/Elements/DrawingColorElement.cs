using System;
using System.Drawing;

namespace Charles.Shipper.Printing.Core.Drawing.Elements
{
	public abstract class DrawingColorElement : DrawingElement
	{
		public int Red { get; set; }
		public int Green { get; set; }
		public int Blue { get; set; }

		public Color Colour { 
			get{
				return Color.FromArgb (Red, Green, Blue);
			}
		}
	}
}

