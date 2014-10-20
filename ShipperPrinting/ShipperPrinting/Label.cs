using System;
using Charles.Shipper.Printing.Core.Interfaces;
using System.Drawing.Printing;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charles.Shipper.Printing.Core
{
	public class Label : ILabel
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

		public Task Task = null;
		private IEnumerable<IDrawingCommand> Commands = null;

		public Label(){

		}

		public Label(string document)
			:this(new DrawingParser(document))
		{

		}

		public Label(DrawingParser parser){
			Task = Task.Factory.StartNew (() => {
				Commands = parser.GetCommands();
			});
		}

		public Label(DrawingDocument document)
			: this(document.GetCommands())
		{

		}
		
		public Label(IEnumerable<IDrawingCommand> commands){
			Commands = commands;
		}

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

		public virtual void Draw (Graphics graphics){
			if (Task != null) {
				Task.Wait ();
			}
			using (DrawingClient client = new DrawingClient(Commands)) {
				client.Draw (graphics);
			}
		}

		public void Dispose(){

		}
	}
}

