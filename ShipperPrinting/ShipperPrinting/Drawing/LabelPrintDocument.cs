using System;
using Charles.Shipper.Printing.Core.Interfaces;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Linq;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core.Drawing
{
	public class LabelPrintDocument : PrintDocument
	{
		public IDrawingClient DrawingClient { get; set; }
		private int Index = 0;

		public IEnumerable<ILabel> Labels {
			get {
				return _Labels;	
			}
		}

		private ILabel[] _Labels { get; set; }
		
		public LabelPrintDocument ()
			:this(new DrawingClient())
		{

		}
		
		public LabelPrintDocument (params ILabel[] labels)
			:this(new DrawingClient(), labels)
		{

		}

		public LabelPrintDocument (IDrawingClient client, params ILabel[] labels)
			:this(client, labels as IEnumerable<ILabel>)
		{

		}

		public LabelPrintDocument (IEnumerable<ILabel> labels)
			:this(new DrawingClient(), labels)
		{

		}

		public LabelPrintDocument (IDrawingClient client, IEnumerable<ILabel> labels)
		{
			_Labels = labels.ToArray();
			DrawingClient = client;
			DefaultPageSettings.Landscape = false;
			DefaultPageSettings.PaperSize = new PaperSize("Shipper Label", 400, 590);
		}

		protected override void OnPrintPage(PrintPageEventArgs e)
		{
			if (Index >= _Labels.Length) {
				e.HasMorePages = false;
				return;
			}
			ILabel label = _Labels.ElementAt (Index);
			using (e.Graphics) {
				try{
					label.Draw(e.Graphics);
				}catch{

				}
			}
			Index += 1;
			e.HasMorePages = Index < _Labels.Length;
			if (Index >= _Labels.Length)
			{
				//Complete
				Index = 0;
			}
		}

		public void Print(string printerName){
			PrinterSettings.PrinterName = printerName;
			Print ();
		}
	}
}

