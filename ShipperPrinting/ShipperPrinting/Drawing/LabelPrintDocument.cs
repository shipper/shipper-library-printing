using System;
using Charles.Shipper.Printing.Core.Interfaces;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Linq;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;

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
		}

		protected override void OnPrintPage(PrintPageEventArgs e)
		{
			if (Index >= _Labels.Length) {
				e.HasMorePages = false;
				return;
			}
			ILabel label = _Labels.ElementAt (Index);
			PrintLabelWithDispose (label, e.Graphics);
			Index += 1;
			e.HasMorePages = Index < _Labels.Length;
			if (Index >= _Labels.Length)
			{
				//Complete
				Index = 0;
			}
		}

		private void PrintLabelWithDispose(ILabel label, Graphics g){
			using(g){
				try{
					PrintLabel (label, g);
				}catch(Exception ex){
					ex.Log ();
				}
			}
		}

		private void PrintLabel(ILabel label, Graphics g){
			label.Draw (g);
		}

		private void SetupPrint(){
			DefaultPageSettings.Landscape = DrawingClient.Landscape;
			DefaultPageSettings.PaperSize = new PaperSize("Shipper Label", 
			                                              DrawingClient.Width, 
			                                              DrawingClient.Height);
		}

		public void Print(string printerName){
			PrinterSettings.PrinterName = printerName;
			PrinterSettings.PrintToFile = false;
			Print ();
		}

		public new void Print(){
			SetupPrint ();
			base.Print ();
		}

		public void PrintToFile(string path, string name){
			path = Path.Combine (path, name);
			PrintToFile (path);
		}
		
		public void PrintToFile(string path){
			SetupPrint ();
			ICollection<Bitmap> bitmaps = new Collection<Bitmap> ();
			ICollection<Graphics> graphics = new Collection<Graphics> ();
			foreach (ILabel label in Labels) {
				Bitmap bitmap = new Bitmap (label.Width, 
				                            label.Height,
				                            PixelFormat.Format32bppArgb);
				bitmaps.Add (bitmap);
				Graphics graphic = Graphics.FromImage(bitmap);
				graphics.Add (graphic);
				try{
					PrintLabel (label, graphic);
				}catch(Exception ex){
					ex.Log ();
				}
			}
			Bitmap result = bitmaps.Join();

			result.Save (path, ImageFormat.Png);
			bitmaps.Dispose ();
			graphics.Dispose ();
		} 
	}
}

