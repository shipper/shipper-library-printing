using System;
using System.Drawing.Printing;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Interfaces
{
	public interface ILabel : IDisposable
	{
		int Width {get;set;}
		int Height {get;set;}
		Font Font {get;set;}
		PrintDocument Document{ get; }
		IDrawingClient Client { get; }
		void Print();
		void Print(string printerName);
		void Draw (Graphics graphics);
		
	}
}

