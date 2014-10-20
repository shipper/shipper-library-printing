using System;
using System.Drawing;

namespace Charles.Shipper.Printing.Core.Drawing.Interfaces
{
	public interface IDrawingCommand
	{
		void Draw(Graphics g);
	}
}

