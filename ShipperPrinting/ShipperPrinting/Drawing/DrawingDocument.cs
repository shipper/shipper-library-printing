using System;
using System.Collections.Generic;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Elements;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;
using System.Linq;

namespace Charles.Shipper.Printing.Core.Drawing
{
	public class DrawingDocument 
	{
		public int OriginX { get; set; }
		public int OriginY { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }
		
		public int BackgroundAlpha { get; set; }
		public int BackgroundRed { get; set; }
		public int BackgroundGreen { get; set; }
		public int BackgroundBlue { get; set; }
		
		public IEnumerable<DrawingString> Strings { get; set; }
		public IEnumerable<DrawingImage> Images { get; set; }
		public IEnumerable<DrawingRectangle> Rectangles { get; set; }
		public IEnumerable<DrawingLine> Lines { get; set; }
		public IEnumerable<DrawingCode128Barcode> Code128Barcodes { get; set; }
		public IEnumerable<DrawingPDF417Barcode> PDF417Barcodes { get; set; }
		
		public IEnumerable<IDynamicElement> Document { get; set; }

		public void Draw(IDrawingClient client){
			if (Document != null && Document.Any ()) {
				DrawDocument (client);
			} else {
				DrawSingle (client);
			}
		}

		private IDictionary<string, IDynamicElement> IdElements { get; set; }
		private IDictionary<string, string> IdElementsDefaults { get; set; }

		private void DrawDocument(IDrawingClient client){
			BuildIdElements ();
			if (!IdElements.Keys.Any () || !Document.Any()) {
				DrawSingle (client);
			}
			IDictionary<string, IDynamicElement> binded = BindKeys (Document);
			var pairs = binded.Pair (IdElements);
			foreach (string key in IdElementsDefaults.Keys) {
				IdElements [key].Content = IdElementsDefaults [key];
			}
			foreach (var pair in pairs) {
				IDynamicElement data = pair.Value.Value1;
				IDynamicElement element = pair.Value.Value2;
				element.Content = data.Content;
			}
			DrawSingle (client);
		}

		private void BuildIdElements(){
			if (IdElements != null) {
				return;
			}
			IEnumerable<DrawingElement> elements = Join (Strings,
			                                             Images,
			                                             Rectangles,
			                                             Lines,
			                                             Code128Barcodes,
			                                             PDF417Barcodes);
			IEnumerable<IDynamicElement> dynamicElements = elements
				.Select (x => x as IDynamicElement)
				.Where (x => x != null);
			IdElements = BindKeys (dynamicElements, IdElementsDefaults = new Dictionary<string, string>());
		}

		private IDictionary<string, IDynamicElement> BindKeys(IEnumerable<IDynamicElement> elements, IDictionary<string, string> defaults = null){
			IDictionary<string, IDynamicElement> result = new Dictionary<string, IDynamicElement> ();
			foreach (IDynamicElement element in elements.Where(x => x != null)) {
				if (String.IsNullOrWhiteSpace (element.Id)) {
					continue;
				}
				if (result.ContainsKey (element.Id)) {
					continue;
				}
				result.Add (element.Id, element);
				if (defaults != null) {
					if (defaults.ContainsKey (element.Id)) {
						defaults [element.Id] = element.Content;
					} else {
						defaults.Add (element.Id, element.Content);
					}
				}
			}
			return result;
		}

		private IEnumerable<DrawingElement> Join(params IEnumerable<DrawingElement>[] elements){
			return elements.Where(e => e != null).SelectMany (e => e);
		}

		private void DrawSingle(IDrawingClient client){
			client.ClearCommands ();
			client.TranslateTransform (OriginX, OriginY);
			client.Width = Width;
			client.Height = Height;
			Color color = Color.FromArgb (BackgroundAlpha, 
			                              BackgroundRed, 
			                              BackgroundGreen, 
			                              BackgroundBlue);
			client.Clear (color);
			Draw (client, Strings);
			Draw (client, Images);
			Draw (client, Rectangles);
			Draw (client, Lines);
			Draw (client, Code128Barcodes);
			Draw (client, PDF417Barcodes);
		}

		public void Draw(Graphics graphics){
			using (DrawingClient client = new DrawingClient()) {
				Draw (client);
				client.Draw (graphics);
			}
		}
		
		internal static void Draw(IDrawingClient client, IEnumerable<DrawingElement> elements){
			if (client == null || elements == null) {
				return;
			}
			foreach (DrawingElement element in elements) {
				Draw (client, element);
			}

		}
		internal static void Draw(IDrawingClient client, DrawingElement element){
			if (client == null || element == null) {
				return;
			}
			if (element.Rotation != 0) {
				client.RotateTransform (element.Rotation);
			}
			element.Draw (client);
			if (element.Rotation != 0) {
				client.RotateTransform (-element.Rotation);
			}
		}

		public IEnumerable<IDrawingCommand> GetCommands(){
			using (DrawingClient client = new DrawingClient()) {
				Draw (client);
				return client.Commands;
			}
		}
	}
}

