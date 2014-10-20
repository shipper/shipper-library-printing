using System;
using System.Collections.Generic;
using Charles.Shipper.Printing.Core.Interfaces;
using Charles.Shipper.Printing.Core.Drawing;

public static class IEnumerableILabelExtensions
{
	public static void Print(this IEnumerable<ILabel> labels){
		labels.ToLabelPrintDocument ().Print ();
	}

	public static void Print(this IEnumerable<ILabel> labels, string printerName){
		labels.ToLabelPrintDocument ().Print (printerName);
	}

	public static void PrintToFile(this IEnumerable<ILabel> labels, string path, string name){
		labels.ToLabelPrintDocument ().PrintToFile (path, name);
	}

	public static void PrintToFile(this IEnumerable<ILabel> labels, string path){
		labels.ToLabelPrintDocument ().PrintToFile (path);
	}

	public static LabelPrintDocument ToLabelPrintDocument(this IEnumerable<ILabel> labels){
		return new LabelPrintDocument (labels);
	}
}
