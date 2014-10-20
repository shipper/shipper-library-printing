using System;
using System.Collections.Generic;
using Charles.Shipper.Printing.Core.Interfaces;
using System.Threading.Tasks;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;
using Charles.Shipper.Printing.Core.Drawing;
using Newtonsoft.Json;

namespace Charles.Shipper.Printing.Core.Drawing
{
	public class DrawingParser
	{
		private Task Task = null;
		private IEnumerable<IDrawingCommand> Commands = null;

		public DrawingParser (string document)
		{
			Task = Task.Factory.StartNew (() => {
				Commands = ProcessDocument(document);
			});
		}

		private  IEnumerable<IDrawingCommand> ProcessDocument(string document){
			using(DrawingClient client = new DrawingClient ()){
				DrawingDocument resultDocument = null;
				try{
					resultDocument = JsonConvert.DeserializeObject<DrawingDocument> (document);
				}catch{
					return client.Commands;
				}
				resultDocument.Draw (client);
				return client.Commands;
			}
		}

		public IEnumerable<IDrawingCommand> GetCommands(){
			Task.Wait ();
			return Commands ?? new IDrawingCommand[0];
		}
	}
}

