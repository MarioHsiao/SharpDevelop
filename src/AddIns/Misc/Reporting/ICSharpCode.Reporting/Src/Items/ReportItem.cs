﻿/*
 * Created by SharpDevelop.
 * User: Peter Forstmeier
 * Date: 06.04.2013
 * Time: 20:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using ICSharpCode.Reporting.Interfaces;
using ICSharpCode.Reporting.Interfaces.Export;

namespace ICSharpCode.Reporting.Items
{
	/// <summary>
	/// Description of ReportItem.
	/// </summary>
	
	
	
	public class ReportItem : IReportObject
	{
		public ReportItem()
		{
		}


		public string Name { get; set; }

		public Point Location { get; set; }

		public Size Size { get; set; }

		
	}
	
	

}
