using System;
using Charles.Shipper.Printing.Core.Interfaces;
using System.Drawing;
using Charles.Shipper.Printing.Core.Drawing.Interfaces;

namespace Charles.Shipper.Printing.Core
{
	internal class DrawingActionCommand : IDrawingCommand
	{
		public IActionCommand<Graphics> ActionCommand { get; private set; }

		public DrawingActionCommand(IActionCommand<Graphics> command){
			ActionCommand = command;
		}

		public void Draw(Graphics g){
			ActionCommand.Call (g);
		}
	}
}

