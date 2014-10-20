using System;
using Charles.Shipper.Printing.Core.Interfaces;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using GenCode128;
using System.IO;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;
using System.Linq;

namespace Charles.Shipper.Printing.Core.Drawing
{
	public class DrawingClient : IDrawingClient, IDisposable
	{

		public DrawingClient()
			: this(null)
		{

		}

		public DrawingClient(IEnumerable<IDrawingCommand> commands)
		{
			if (commands != null) {
				Commands = commands.ToList ();
			}
		}

		public int Width {get;set;}
		public int Height {get;set;}
		public Font DefaultFont { get; set; }
		public Brush DefaultBrush { get; set; }
		public Pen DefaultPen { get; set; }
		public bool DisposeAllChildren = true;

		internal ICollection<IDrawingCommand> Commands = new Collection<IDrawingCommand>();
		internal ICollection<IDisposable> DisposableObjects = new Collection<IDisposable>();

		public void TranslateTransform(float dx, float dy){
			AddCommand ((graphics) => {
				graphics.TranslateTransform (dx, dy);
			});
		}

		public void RotateTransform(float angle){
			AddCommand ((graphics) => {
				graphics.RotateTransform (angle);
			});
		}

		public void DrawRectangle(int x, int y, int width, int height){
			DrawRectangle (DefaultPen, x, y, width, height);
		}

		public void DrawRectangle(float x, float y, float width, float height){
			DrawRectangle (DefaultPen, x, y, width, height);
		}

		public void DrawRectangle(Pen pen, int x, int y, int width, int height){
			AddCommand ((graphics) => {
				graphics.DrawRectangle (pen, x, y, width, height);
			});
		}

		public void DrawRectangle(Pen pen, float x, float y, float width, float height){
			AddCommand ((graphics) => {
				graphics.DrawRectangle (pen, x, y, width, height);
			});
		}

		public void DrawLine(int x1, int y1, int x2, int y2){
			DrawLine (DefaultPen, x1, y1, x2, y2);
		}

		public void DrawLine(float x1, float y1, float x2, float y2){
			DrawLine (DefaultPen, x1, y1, x2, y2);
		}

		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2){
			AddCommand ((graphics) => {
				graphics.DrawLine(pen, x1, y1, x2, y2);
			});
		}

		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2){
			AddCommand ((graphics) => {
				graphics.DrawLine(pen, x1, y1, x2, y2);
			});
		}


		public void DrawString(String value, PointF point){
			DrawString (value, DefaultFont, DefaultBrush, point);
		}

		public void DrawString(String value, RectangleF point){
			DrawString (value, DefaultFont, DefaultBrush, point);
		}

		public void DrawString(String value, PointF point, StringFormat format){
			DrawString (value, DefaultFont, DefaultBrush, point, format);
		}

		public void DrawString(String value, RectangleF point, StringFormat format){
			DrawString (value, DefaultFont, DefaultBrush, point, format);
		}

		public void DrawString(String value, float x, float y){
			DrawString (value, DefaultFont, DefaultBrush, x, y);
		}

		public void DrawString(String value, float x, float y, StringFormat format){
			DrawString (value, DefaultFont, DefaultBrush, x, y, format);
		}

		public void DrawString(String value, Font font, Brush brush, PointF point){
			AddCommand ((graphics) => {
				graphics.DrawString (value, font, brush, point);
			});
		}

		public void DrawString(String value, Font font, Brush brush, RectangleF point){
			AddCommand ((graphics) => {
				graphics.DrawString (value, font, brush, point);
			});
		}

		public void DrawString(String value, Font font, Brush brush, PointF point, StringFormat format){
			AddCommand ((graphics) => {
				graphics.DrawString (value, font, brush, point, format);
			});
		}

		public void DrawString(String value, Font font, Brush brush, RectangleF point, StringFormat format){
			AddCommand ((graphics) => {
				graphics.DrawString (value, font, brush, point, format);
			});
		}

		public void DrawString(String value, Font font, Brush brush, float x, float y){
			AddCommand ((graphics) => {
				graphics.DrawString (value, font, brush, x, y);
			});
		}

		public void DrawString(String value, Font font, Brush brush, float x, float y, StringFormat format){
			AddCommand ((graphics) => {
				graphics.DrawString (value, font, brush, x, y, format);
			});
		}

		public void DrawCode128Barcode(String value, int x, int y, RotateFlipType rotateFlipType){
			DrawCode128Barcode (value, x, y, 1, false, rotateFlipType);
		}

		public void DrawCode128Barcode(String value, int x, int y, int barWeight = 1, bool addQuietZone = false, RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone){
			AddCommand ((graphics) => {
				using(Image image = Code128Rendering.MakeBarcodeImage(value, barWeight, addQuietZone)){
					if(rotateFlipType != RotateFlipType.RotateNoneFlipNone){
						image.RotateFlip(rotateFlipType);
					}
					graphics.DrawImageUnscaled(image, x, y);
				}
			});
		}

		public void DrawPDF417Barcode(String value, int x, int y, RotateFlipType rotateFlipType){
			DrawPDF417Barcode (value, x, y, 1, 1, rotateFlipType);
		}

		public void DrawPDF417Barcode(String value, int x, int y, int scaleX = 1, int scaleY = 1, RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone){
			AddCommand ((graphics) => {
				Pdf417lib pd = new Pdf417lib();
				pd.setText(value);
				pd.CodeRows = 2;
				pd.CodeColumns = 6;
				pd.Options = Pdf417lib.PDF417_INVERT_BITMAP | Pdf417lib.PDF417_FIXED_RECTANGLE;
				pd.paintCode();

				using(Image image = pd.DrawBarcodeImage(scaleX, scaleY)){
					if(rotateFlipType != RotateFlipType.RotateNoneFlipNone){
						image.RotateFlip(rotateFlipType);
					}
					graphics.DrawImageUnscaled(image, x, y);
				}
			});
		}

		public void DrawImageUnscaled(Image image, int x, int y){
			DrawImageUnscaled (image, x, y, RotateFlipType.RotateNoneFlipNone);
		}

		public void DrawImageUnscaled(Image image, int x, int y, RotateFlipType rotateFlipType){
			if(rotateFlipType != RotateFlipType.RotateNoneFlipNone){
				image.RotateFlip(rotateFlipType);
			}
			AddCommand ((graphics) => {
				graphics.DrawImageUnscaled(image, x, y);
			});
			if (DisposeAllChildren) {
				DisposableObjects.Add (image);
			}
		}

		public void DrawImage(string base64, int x, int y){
			DrawImage (base64, x, y, RotateFlipType.RotateNoneFlipNone);
		}

		public void DrawImage(string base64, int x, int y, RotateFlipType rotateFlipType){
			byte[] bytes = Convert.FromBase64String (base64);
			using (MemoryStream stream = new MemoryStream (bytes, 0, bytes.Length)) {
				stream.Write (bytes, 0, bytes.Length);
				DrawImage (stream, x, y, rotateFlipType, false);
			}
			bytes = null;
		}

		public void DrawImage(MemoryStream image, int x, int y){
			DrawImage (image, x, y, RotateFlipType.RotateNoneFlipNone);
		}

		public void DrawImage(MemoryStream image, int x, int y, RotateFlipType rotateFlipType, bool dispose = true){
			if (dispose) {
				using (image) {
					DrawImage (image, x, y, rotateFlipType, false);
				}
			}
			Image actualImage = Image.FromStream (image, true);
			DrawImage (actualImage, x, y, rotateFlipType);
		}

		public void DrawImage(Image image, int x, int y){
			DrawImage (image, x, y, RotateFlipType.RotateNoneFlipNone);
		}

		public void DrawImage(Image image, int x, int y, RotateFlipType rotateFlipType){
			if(rotateFlipType != RotateFlipType.RotateNoneFlipNone){
				image.RotateFlip(rotateFlipType);
			}
			AddCommand ((graphics) => {
				graphics.DrawImage(image, x, y);
			});
			if (DisposeAllChildren) {
				DisposableObjects.Add (image);
			}
		}
		
		internal void AddCommand(Action<Graphics> action){
			AddCommand (new ActionCommand<Graphics> (action, null));
		}

		internal void AddCommand(IActionCommand<Graphics> action){
			AddCommand (new DrawingActionCommand (action));
		}

		public void AddCommand(IDrawingCommand command){
			Commands.Add (command);
		}

		public void Draw(Graphics graphics){
			foreach (IDrawingCommand command in Commands) {
				if (command != null) {
					command.Draw (graphics);
				}
			}
		}

		public void Dispose(){
			if (DisposeAllChildren) {
				foreach (IDisposable disposable in DisposableObjects) {
					try{
						disposable.Dispose();
					}catch{

					}
				}
			}
		}


	}
}

