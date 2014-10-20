using System;
using Charles.Shipper.Printing.Core.Interfaces;
using System.Drawing.Printing;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

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

		private Action<Graphics> DrawingAction = null;

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
			Width = document.Width;
			Height = document.Height;
		}
		
		public Label(IEnumerable<IDrawingCommand> commands){
			Commands = commands;
		}

		private Label(Action<Graphics> drawingAction){
			DrawingAction = drawingAction;
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
		
		public void PrintToFile(string path, string name){
			MakeDocument ();
			_Document.PrintToFile (path, name);
		}

		public void PrintToFile(string path){
			MakeDocument ();
			_Document.PrintToFile (path);
		}

		public virtual void Draw (Graphics graphics){
			if (Task != null) {
				Task.Wait ();
			}
			if (DrawingAction != null) {
				DrawingAction (graphics);
				return;
			}
			using (DrawingClient client = new DrawingClient(Commands)) {
				client.Width = Width;
				client.Height = Height;
				client.Draw (graphics);
			}
		}

		public void Dispose(){

		}

		public static IEnumerable<Label> FromDocuments(DrawingDocument document, params IEnumerable<IDynamicElement>[] documents){
			if (documents == null || !documents.Any ()) {
				return new []{ 
					new Label (document) 
				};
			}
			return documents.Select (doc => {
				Label label = new Label((graphics)=>{
					document.Document = doc;
					using(DrawingClient client = new DrawingClient(document.GetCommands())){
						client.Draw(graphics);
					}
				});
				label.Width = document.Width;
				label.Height = document.Height;
				return label;
			});
		}

	}
}

