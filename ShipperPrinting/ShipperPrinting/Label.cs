using System;
using Charles.Shipper.Printing.Core.Interfaces;
using System.Drawing.Printing;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core
{
	public abstract class Label : ILabel
	{
		public int Width {get;set;}
		public int Height {get;set;}
		public Font Font {get;set;}
		private LabelPrintDocument _Document = null;
		public PrintDocument Document {
			get {
				MakeDocument ();
				return _Document;
			}
		}
		public IDrawingClient Client { get; private set; }

		private void MakeDocument(){
			if (_Document == null) {
				_Document = new LabelPrintDocument (this);
			}
		}

		public void Print(){
			MakeDocument ();
			_Document.Print ();
		}

		public void Print(string printerName){
			MakeDocument ();
			_Document.Print (printerName);
		}

		public void Draw (Graphics graphics){
			Client.Draw (graphics);
		}

		public void Dispose(){
			IDisposable client = Client as IDisposable;
			if (client != null) {
				client.Dispose ();
			}
		}
	}
}

