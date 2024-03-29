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
		PrintDocument Document { get; }
		void Print ();
		void Print (string printerName);
		void PrintToFile (string path, string name);
		void PrintToFile (string path);
		void Draw (Graphics graphics);
		
	}
}

